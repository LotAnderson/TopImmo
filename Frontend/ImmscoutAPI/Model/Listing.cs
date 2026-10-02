using System.Net;
using System.Text.Json.Serialization;

namespace ImmscoutAPI.Model
{
    public class Listing
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("livingSpace")]
        public double LivingSpace { get; set; }

        [JsonPropertyName("rooms")]
        public double Rooms { get; set; }

        [JsonPropertyName("realEstateType")]
        public string RealEstateType { get; set; }

        [JsonPropertyName("listingType")]
        public string ListingType { get; set; }

        [JsonPropertyName("isPrivate")]
        public bool IsPrivate { get; set; }

        [JsonPropertyName("isProject")]
        public bool IsProject { get; set; }

        [JsonPropertyName("isNew")]
        public bool IsNew { get; set; }

        [JsonPropertyName("liveVideoTourAvailable")]
        public bool LiveVideoTourAvailable { get; set; }

        [JsonPropertyName("publishedLabel")]
        public string PublishedLabel { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("pictureUrls")]
        public List<string> PictureUrls { get; set; } = new();

        [JsonPropertyName("attributes")]
        public List<AttributeItem> Attributes { get; set; } = new();

        [JsonPropertyName("realtRealtorLogoUrl")]
        public string RealtorLogoUrl { get; set; }

        // --- Вычисляемые/присваиваемые поля для карты ---

        /// <summary>
        /// Район (например, Mitte, Wilmersdorf, Köpenick), извлеченный из адресной строки.
        /// </summary>
        public string MappedDistrict { get; set; }
    }
}
