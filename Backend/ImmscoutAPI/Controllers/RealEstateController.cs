using ImmscoutAPI.Model;
using ImmscoutAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmscoutAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RealEstateController(DistrictDataService districtDataService) : ControllerBase
{
    [HttpGet("stuttgart-listings")]
    public async Task<ActionResult<ListingSearchResult>> GetStuttgartListings([FromQuery] string? district)
    {
        return Ok(await districtDataService.SearchAsync(district));
    }

    [HttpGet("listings/{id}")]
    public async Task<ActionResult<Listing>> GetListing(string id)
    {
        var listings = await districtDataService.GetProcessedListingsAsync();
        var listing = listings.FirstOrDefault(item => item.Id == id);
        return listing is null ? NotFound() : Ok(listing);
    }
}
