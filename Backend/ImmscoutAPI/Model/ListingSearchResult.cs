namespace ImmscoutAPI.Model;

public record DistrictCount(string District, int Count);
public record ListingSearchResult(
    List<Listing> Listings,
    List<DistrictCount> DistrictCounts,
    IReadOnlyDictionary<string, string> MapDistricts);
