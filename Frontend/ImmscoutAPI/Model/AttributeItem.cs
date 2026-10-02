using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class AttributeItem
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
