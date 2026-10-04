using System.Reflection;
using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class ImmoScoutResponse
    {
        [JsonPropertyName("location")]
        public LocationInfo Location { get; set; }

        [JsonPropertyName("pagination")]
        public PaginationInfo Pagination { get; set; }

        [JsonPropertyName("listings")]
        public List<Listing> Listings { get; set; }

        [JsonPropertyName("projects")]
        public List<Project> Projects { get; set; }
    }
}
