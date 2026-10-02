using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class Address
    {
        [JsonPropertyName("line")]
        public string Line { get; set; }

        [JsonPropertyName("lat")]
        public double? Lat { get; set; }

        [JsonPropertyName("lon")]
        public double? Lon { get; set; }
    }
}
