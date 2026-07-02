using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class LocationInfo
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
