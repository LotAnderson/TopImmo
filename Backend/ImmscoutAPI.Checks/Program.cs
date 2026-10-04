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
