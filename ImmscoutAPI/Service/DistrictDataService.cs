using System.Text.Json;
using ImmscoutAPI.Model;

namespace ImmscoutAPI.Service
{
    public class DistrictDataService
    {
        private readonly ImmoScoutAPIService _apiService;

        public DistrictDataService(ImmoScoutAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Listing>> GetProcessedListingsAsync()
        {
            
            var jsonString = await _apiService.GetStuttgartApartmentsAsync();

            // 2. Сериализуем (десериализуем) ответ
            var response = JsonSerializer.Deserialize<ImmoScoutResponse>(jsonString);

            if (response?.Listings == null)
                return new List<Listing>();

            // 3. Присваиваем район каждому объекту недвижимости
            foreach (var listing in response.Listings)
            {
                listing.MappedDistrict = ExtractDistrictFromAddress(listing.Address?.Line);
            }

            return response.Listings;
        }

        private string ExtractDistrictFromAddress(string addressLine)
        {
            if (string.IsNullOrWhiteSpace(addressLine))
                return "Unknown";

            // Пример для Штуттутгарта: строка адреса обычно заканчивается на индекс, город и район, 
            // например: "Kronenstraße 25, 70174 Stuttgart, Stuttgart-Mitte"
            var parts = addressLine.Split(',');

            // Если в строке 3 и более частей, берем последнюю (район)
            if (parts.Length > 2)
            {
                return parts[^1].Trim();
            }

            return "Stuttgart";
        }
    }
}