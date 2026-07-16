using ImmscoutAPI.Model;
using ImmscoutAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmscoutAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RealEstateController : ControllerBase
    {
        private readonly DistrictDataService _districtDataService;

        // Внедряем наш сервис-обработчик
        public RealEstateController(DistrictDataService districtDataService)
        {
            _districtDataService = districtDataService;
        }

        // Эндпоинт: GET /api/realestate/stuttgart-listings
        [HttpGet("stuttgart-listings")]
        public async Task<ActionResult<List<Listing>>> GetStuttgartListings()
        {
            try
            {
                var listings = await _districtDataService.GetProcessedListingsAsync();

                if (listings == null || listings.Count == 0)
                {
                    return NotFound("Keine Immobilie wird gefunden");
                }

                return Ok(listings);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
