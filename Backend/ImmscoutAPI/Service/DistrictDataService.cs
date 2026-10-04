using Microsoft.Extensions.Caching.Memory;
﻿using System.Text.Json;
using ImmscoutAPI.Model;

namespace ImmscoutAPI.Service
{
    public class DistrictDataService
    {
        private readonly ImmoScoutAPIService _apiService;
        private readonly Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;
        private readonly IReadOnlyDictionary<string, string> _mapDistricts;
        private static readonly SemaphoreSlim LoadLock = new(1, 1);

        public DistrictDataService(ImmoScoutAPIService apiService, Microsoft.Extensions.Caching.Memory.IMemoryCache cache, IWebHostEnvironment environment)
        {
            _apiService = apiService;
            _cache = cache;
            _mapDistricts = JsonSerializer.Deserialize<Dictionary<string, string>>(
                File.ReadAllText(Path.Combine(environment.ContentRootPath, "Data", "map-districts.json")))!;
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

            // Пример для Штуттутгарта: строка адреса обычно заканчивается на индекс, город и район, 
            // например: "Kronenstraße 25, 70174 Stuttgart, Stuttgart-Mitte"
            var parts = addressLine.Split(',');

            // Если в строке 3 и более частей, берем последнюю (район)
            if (parts.Length > 2)
            {
                var district = parts[^1].Trim();
                if (district.StartsWith("Stuttgart-", StringComparison.OrdinalIgnoreCase))
                    district = district["Stuttgart-".Length..];
                return _mapDistricts.Values.FirstOrDefault(name =>
                    string.Equals(name, district, StringComparison.OrdinalIgnoreCase)) ?? district;
            }

            return "Stuttgart";
        }
    }
}