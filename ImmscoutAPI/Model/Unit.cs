using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class Unit
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; set; }
    }
}
