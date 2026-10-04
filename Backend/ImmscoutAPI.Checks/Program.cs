using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using ImmscoutAPI.Model;
using System.Net;
using ImmscoutAPI.Service;
using ImmscoutAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text.RegularExpressions;

var backendPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../ImmscoutAPI"));
var environment = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = backendPath }).Environment;
using var cache = new MemoryCache(new MemoryCacheOptions());
var handler = new FixtureHandler();
var upstreamConfiguration = new ConfigurationBuilder().AddInMemoryCollection(
    new Dictionary<string, string> { ["RapidApi:ApiKey"] = "fixture-api-key" }).Build();
var apiService = new ImmoScoutAPIService(new HttpClient(handler), upstreamConfiguration);
var service = new DistrictDataService(apiService, cache, environment);
var searches = await Task.WhenAll(service.SearchAsync(null), service.SearchAsync("mitte"));
var all = searches[0];
Assert(handler.LastApiKey == "fixture-api-key", "Configured API key is sent to upstream");
var callsBeforeMissingKey = handler.Calls;
try
{
    await new ImmoScoutAPIService(new HttpClient(handler), new ConfigurationBuilder().Build())
        .GetStuttgartApartmentsAsync();
    throw new Exception("Missing API key should be rejected before HTTP");
}
catch (InvalidOperationException) { }
Assert(handler.Calls == callsBeforeMissingKey, "Missing API key does not call upstream");
Assert(all.Listings.Count == 3, "Only rental apartments are returned");
Assert(searches[1].Listings.Count == 2, "District filtering normalizes Stuttgart prefix and casing");
Assert(all.DistrictCounts.First().District == "Mitte" && all.DistrictCounts.First().Count == 2, "Counts are sorted and aggregate rentals");
Assert(searches[1].DistrictCounts.Sum(item => item.Count) == 3, "Counts remain city-wide when filtering");
Assert(all.MapDistricts["a101_Oberer_Schlossgarten"] == "Mitte", "Map IDs are mapped by backend");
Assert((await service.SearchAsync("missing")).Listings.Count == 0, "Empty searches succeed");
Assert(handler.Calls == 1, "Concurrent and repeated searches share cached upstream data");
var controller = new RealEstateController(service);
Assert((await controller.GetListing("1")).Result is OkObjectResult, "Detail endpoint returns an existing listing");
Assert((await controller.GetListing("missing")).Result is NotFoundResult, "Detail endpoint returns 404 for a missing listing");
cache.Remove("stuttgart-listings");
await service.SearchAsync(null);
Assert(handler.Calls == 2, "Expired cache can refresh");
cache.Remove("stuttgart-listings");
handler.FailNext = true;
try
{
    await service.SearchAsync(null);
    throw new Exception("Upstream failure should propagate");
}
catch (HttpRequestException) { }
Assert((await service.SearchAsync(null)).Listings.Count == 3, "Failed upstream loads do not poison cache or lock");
cache.Remove("stuttgart-listings");
handler.Body = "{\"listings\":[{\"id\":\"no-address\",\"realEstateType\":\"apartmentrent\"}]}";
Assert((await service.SearchAsync(null)).Listings.Single().MappedDistrict == "Unknown", "Missing addresses are safe");
cache.Remove("stuttgart-listings");
handler.Body = "{\"listings\":[]}";
Assert((await service.SearchAsync(null)).DistrictCounts.Count == 0, "Empty upstream data returns empty counts");
cache.Remove("stuttgart-listings");

// The canonical reference was independently checked against the city of
// Stuttgart's complete list. Exercise every Stadtteil through the real search
// pipeline, including known district boundaries and ambiguous location data.
var officialSubdistricts = JsonSerializer.Deserialize<List<OfficialSubdistrict>>(
    File.ReadAllText(Path.Combine(backendPath, "Data", "stuttgart-subdistricts.json")),
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
var expectedDistrictSizes = new Dictionary<string, int>
{
    ["Mitte"] = 10, ["Nord"] = 11, ["Ost"] = 8, ["Süd"] = 7, ["West"] = 9,
    ["Bad Cannstatt"] = 18, ["Birkach"] = 3, ["Botnang"] = 4, ["Degerloch"] = 5,
    ["Feuerbach"] = 8, ["Hedelfingen"] = 4, ["Möhringen"] = 9, ["Mühlhausen"] = 5,
    ["Münster"] = 1, ["Obertürkheim"] = 2, ["Plieningen"] = 5, ["Sillenbuch"] = 3,
    ["Stammheim"] = 2, ["Untertürkheim"] = 8, ["Vaihingen"] = 12, ["Wangen"] = 1,
    ["Weilimdorf"] = 6, ["Zuffenhausen"] = 11
};
Assert(officialSubdistricts.Count == 152 && officialSubdistricts.Select(item => item.Number).Distinct().Count() == 152,
    "The canonical reference contains all 152 unique official Stadtteile");
Assert(officialSubdistricts.Select(item => item.Name).Distinct().Count() == 152,
    "Official Stadtteil names are unambiguous");
Assert(officialSubdistricts.Select(item => item.District).Distinct().Count() == 23 &&
    expectedDistrictSizes.All(pair => officialSubdistricts.Count(item => item.District == pair.Key) == pair.Value),
    "The reference matches all 23 official district sizes");
Assert(officialSubdistricts.Single(item => item.Name == "Killesberg").Number == 124 &&
    officialSubdistricts.Single(item => item.Name == "Gehrenwald").Number == 661,
    "Official numbering resolves two typos on the city overview with its street directory");
foreach (var entry in all.MapDistricts)
{
    var number = int.Parse(entry.Key.AsSpan(1, 3));
    var official = officialSubdistricts.Single(item => item.Number == number);
    var mapName = Regex.Replace(entry.Key[5..], @"_\d+_$", "")
        .Replace("_x5F_", "_").Replace("x5F_", "").Replace("_x2F_", "/").Replace('_', ' ').Trim();
    Assert(mapName == official.Name && entry.Value == official.District,
        $"Map ID {entry.Key} matches its official Stadtteil name and parent district");
}
Assert(all.MapDistricts.Count == 458 && officialSubdistricts.All(item =>
    all.MapDistricts.Keys.Any(id => id.StartsWith($"a{item.Number}_", StringComparison.Ordinal))),
    "All 458 SVG mapping IDs cover every official Stadtteil");
var locationCases = officialSubdistricts.Select(item =>
    (Address: $"Beispielstraße 1, 70173 Stuttgart, {item.Name}", Expected: item.District)).ToList();
locationCases.AddRange(expectedDistrictSizes.Keys.Select(district =>
    (Address: $"Beispielstraße 1, 70173 Stuttgart, Stuttgart-{district}", Expected: district)));
locationCases.AddRange(new[]
{
    ("Beispielstraße 1, 70329 Stuttgart, Uhlbach", "Obertürkheim"),
    ("Beispielstraße 1, 70327 Stuttgart, Untertürkheim", "Untertürkheim"),
    ("Beispielstraße 1, 70173 Stuttgart, sTuTtGaRt - mItTe", "Mitte"),
    ("Beispielstraße 1, 70173 Stuttgart, Uhlbach (Stuttgart), Deutschland", "Obertürkheim"),
    ("Beispielstraße 1, 70173 Stuttgart, Stuttgart–Mitte", "Mitte"),
    ("Beispielstraße 1, D-70173 Stuttgart-Mitte", "Mitte"),
    ("Beispielstraße 1, 70173 Stuttgart, Universität".Normalize(System.Text.NormalizationForm.FormD), "Mitte"),
    ("Beispielstraße 1, 70173 Stuttgart", "Unknown"),
    ("Beispielstraße 1, 70173 Stuttgart, Stuttgart", "Unknown"),
    ("Beispielstraße 1, 70173 Stuttgart, unbekannter Ort", "Unknown"),
    ("Beispielstraße 1, 70329 Stuttgart", "Unknown"),
    ("Rathaus, 70173 Stuttgart", "Unknown"),
    ("Beispielstraße 1, 70173 Stuttgart, Stuttgart-Mitte, Uhlbach", "Unknown"),
    ("", "Unknown")
});
handler.Body = JsonSerializer.Serialize(new
{
    listings = locationCases.Select((item, index) => new
    {
        id = $"location-{index}", realEstateType = "apartmentrent", address = new { line = item.Address }
    })
});
var locations = await service.SearchAsync(null);
for (var index = 0; index < locationCases.Count; index++)
    Assert(locations.Listings[index].MappedDistrict == locationCases[index].Expected,
        $"Address case {index}: maps complete official locations and preserves unknown data");
Assert(locations.DistrictCounts.All(item => expectedDistrictSizes.ContainsKey(item.District) || item.District == "Unknown"),
    "City-wide counts contain only official districts or Unknown");
foreach (var district in expectedDistrictSizes.Keys)
{
    var filteredLocations = await service.SearchAsync(district);
    Assert(filteredLocations.Listings.Count == locationCases.Count(item => item.Expected == district) &&
        filteredLocations.DistrictCounts.Sum(item => item.Count) == locationCases.Count,
        $"Filtering {district} includes its Stadtteile and retains city-wide counts");
}
Console.WriteLine($"Official location audit passed: 23 districts, 152 Stadtteile, 458 map IDs, {locationCases.Count} address cases.");
cache.Remove("stuttgart-listings");
handler.Body = null;
await service.SearchAsync(null);
// Exercise real controller routing, authorization and JSON serialization over HTTP.
var builder = WebApplication.CreateBuilder(new WebApplicationOptions { ContentRootPath = backendPath });
builder.Logging.ClearProviders();
builder.WebHost.UseUrls("http://127.0.0.1:0");
builder.Services.AddSingleton(service);
builder.Services.AddControllers().AddApplicationPart(typeof(RealEstateController).Assembly);
builder.Services.AddAuthentication("Checks").AddScheme<AuthenticationSchemeOptions, CheckAuthenticationHandler>("Checks", _ => { });
builder.Services.AddAuthorization();
await using var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.StartAsync();
try
{
    using var client = new HttpClient { BaseAddress = new Uri(app.Urls.Single()) };
    Assert((await client.GetAsync("/api/realestate/stuttgart-listings")).StatusCode == HttpStatusCode.Unauthorized,
        "Listing endpoint requires authentication");
    Assert((await client.GetAsync("/api/realestate/listings/1")).StatusCode == HttpStatusCode.Unauthorized,
        "Detail endpoint requires authentication");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer fixture-token");
    var result = await client.GetFromJsonAsync<ListingSearchResult>("/api/realestate/stuttgart-listings?district=Mitte");
    Assert(result!.Listings.Count == 2 && result.DistrictCounts.Sum(item => item.Count) == 3,
        "HTTP query binding and search serialization preserve filtered results and city counts");
    var json = await client.GetStringAsync("/api/realestate/stuttgart-listings");
    Assert(json.Contains("\"districtCounts\"") && json.Contains("\"mapDistricts\"") && json.Contains("\"mappedDistrict\""),
        "JSON property names match frontend contract");
    Assert((await client.GetFromJsonAsync<ListingSearchResult>("/api/realestate/stuttgart-listings?district=missing"))!.Listings.Count == 0,
        "Empty search returns HTTP 200");
    Assert((await client.GetFromJsonAsync<Listing>("/api/realestate/listings/1"))!.Id == "1", "HTTP detail lookup succeeds");
    Assert((await client.GetAsync("/api/realestate/listings/missing")).StatusCode == HttpStatusCode.NotFound,
        "HTTP missing detail returns 404");
}
finally
{
    await app.StopAsync();
}
Console.WriteLine("All listing processing and HTTP integration checks passed.");

static void Assert(bool condition, string message)
{
    if (!condition) throw new Exception(message);
}

sealed record OfficialSubdistrict(int Number, string Name, string District);

sealed class FixtureHandler : HttpMessageHandler
{
    public int Calls;
    public string LastApiKey = string.Empty;
    public bool FailNext;
    public string Body;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastApiKey = request.Headers.GetValues("x-rapidapi-key").Single();
        Interlocked.Increment(ref Calls);
        await Task.Delay(20, cancellationToken);
        if (FailNext)
        {
            FailNext = false;
            throw new HttpRequestException("Fixture upstream failure");
        }
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(Body ?? """
            {"listings":[
              {"id":"1","realEstateType":"apartmentrent","address":{"line":"Street, Stuttgart, Stuttgart-Mitte"}},
              {"id":"2","realEstateType":"APARTMENTRENT","address":{"line":"Street, Stuttgart, Mitte"}},
              {"id":"3","realEstateType":"apartmentrent","address":{"line":"Street, Stuttgart, Nord"}},
              {"id":"4","realEstateType":"apartmentbuy","address":{"line":"Street, Stuttgart, Mitte"}}
            ]}
            """)
        };
    }
}

sealed class CheckAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Request.Headers.Authorization != "Bearer fixture-token")
            return Task.FromResult(AuthenticateResult.NoResult());
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "test-user") }, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name)));
    }
}
