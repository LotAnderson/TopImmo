import { Listing } from './Listing';

export interface ListingSearchResult {
  listings: Listing[];
  districtCounts: { district: string; count: number }[];
  mapDistricts: Record<string, string>;
}
