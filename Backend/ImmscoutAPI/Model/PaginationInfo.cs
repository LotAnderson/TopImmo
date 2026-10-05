using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class PaginationInfo
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }
    }
}
