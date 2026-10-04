using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;
using ImmscoutAPI.Model;

namespace ImmscoutAPI.Service
{
    public class DistrictDataService
    {
        private readonly ImmoScoutAPIService _apiService;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private readonly IReadOnlyDictionary<string, string> _mapDistricts;
        private readonly IReadOnlyDictionary<string, string> _addressDistricts;
        private static readonly SemaphoreSlim LoadLock = new(1, 1);

        public DistrictDataService(ImmoScoutAPIService apiService, Microsoft.Extensions.Caching.Memory.IMemoryCache cache, IWebHostEnvironment environment)
        {
            _apiService = apiService;
            _cache = cache;
            _mapDistricts = JsonSerializer.Deserialize<Dictionary<string, string>>(
                File.ReadAllText(Path.Combine(environment.ContentRootPath, "Data", "map-districts.json")))!;
            var subdistricts = JsonSerializer.Deserialize<List<CitySubdistrict>>(
                File.ReadAllText(Path.Combine(environment.ContentRootPath, "Data", "stuttgart-subdistricts.json")),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            var addressDistricts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var district in _mapDistricts.Values.Distinct())
                addressDistricts.Add(NormalizeLocationName(district), district);
            foreach (var subdistrict in subdistricts)
                addressDistricts[NormalizeLocationName(subdistrict.Name)] = subdistrict.District;
            _addressDistricts = addressDistricts;
        }

        public async Task<List<Listing>> GetProcessedListingsAsync()
        {
            if (_cache.TryGetValue("stuttgart-listings", out List<Listing>? cached))
                return cached!;

            await LoadLock.WaitAsync();
            try
            {
                if (_cache.TryGetValue("stuttgart-listings", out cached))
                    return cached!;

                var json = await _apiService.GetStuttgartApartmentsAsync();
                var response = JsonSerializer.Deserialize<ImmoScoutResponse>(json);
                var listings = (response?.Listings ?? new List<Listing>())
                    .Where(listing => string.Equals(listing.RealEstateType, "apartmentrent", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                foreach (var listing in listings)
                    listing.MappedDistrict = ExtractDistrictFromAddress(listing.Address?.Line);

                Microsoft.Extensions.Caching.Memory.CacheExtensions.Set(
                    _cache, "stuttgart-listings", listings, TimeSpan.FromMinutes(5));
                return listings;
            }
            finally
            {
                LoadLock.Release();
            }
        }

        public async Task<ListingSearchResult> SearchAsync(string? district)
        {
            var listings = await GetProcessedListingsAsync();
            var counts = listings.GroupBy(listing => listing.MappedDistrict)
                .Select(group => new DistrictCount(group.Key, group.Count()))
                .OrderByDescending(item => item.Count).ThenBy(item => item.District).ToList();
            var filtered = string.IsNullOrWhiteSpace(district) ? listings : listings
                .Where(listing => string.Equals(listing.MappedDistrict, district, StringComparison.OrdinalIgnoreCase)).ToList();
            return new ListingSearchResult(filtered, counts, _mapDistricts);
        }

        private string ExtractDistrictFromAddress(string? addressLine)
        {
            if (string.IsNullOrWhiteSpace(addressLine))
                return "Unknown";

            // Match complete location components only: never infer a district from
            // a street name, postcode, or a listing's title. The upstream source
            // sometimes supplies a Stadtteil instead of its parent Stadtbezirk.
            var parts = addressLine.Split(',');
            var matches = parts.Skip(parts.Length > 1 ? 1 : 0)
                .Select(NormalizeLocationName)
                .Where(_addressDistricts.ContainsKey)
                .Select(name => _addressDistricts[name])
                .Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            return matches.Count == 1 ? matches[0] : "Unknown";
        }

        private static string NormalizeLocationName(string value)
        {
            var normalized = Regex.Replace(value.Trim().Normalize(NormalizationForm.FormC), @"\s+", " ")
                .Replace('–', '-').Replace('—', '-');
            normalized = Regex.Replace(normalized, @"^(?:D-)?\d{5}\s+", "");
            normalized = Regex.Replace(normalized, @"^Stuttgart\s*-\s*", "", RegexOptions.IgnoreCase);
            return Regex.Replace(normalized, @"\s*\(Stuttgart\)$", "", RegexOptions.IgnoreCase);
        }

        private sealed record CitySubdistrict(int Number, string Name, string District);
    }
}
