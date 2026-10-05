using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class Project
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("pictureUrls")]
        public List<string> PictureUrls { get; set; } = new();

        [JsonPropertyName("attributes")]
        public List<AttributeItem> Attributes { get; set; } = new();

        [JsonPropertyName("realtorLogoUrl")]
        public string RealtorLogoUrl { get; set; }

        [JsonPropertyName("units")]
        public List<Unit> Units { get; set; } = new();
    }

   

}
