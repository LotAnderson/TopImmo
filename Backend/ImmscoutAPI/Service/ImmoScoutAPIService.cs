using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImmscoutAPI.Service
{
    public class ImmoScoutAPIService
    {
        // Создаем статический или используем фабрику HttpClient во избежание истощения сокетов
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public ImmoScoutAPIService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _apiKey = configuration["RapidApi:ApiKey"] ?? string.Empty;
        }

        public async Task<string> GetStuttgartApartmentsAsync()
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("Configure RapidApi:ApiKey in appsettings.Local.json or RapidApi__ApiKey in the environment.");
            }

            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://immoscout24-api.p.rapidapi.com/v1/search?realEstateType=apartmentrent&page=1&priceType=calculatedtotalrent&pageSize=30&location=Stuttgart&country=de&sort=standard"),
                Headers =
                {
                    { "x-rapidapi-key", _apiKey },
                    { "x-rapidapi-host", "immoscout24-api.p.rapidapi.com" }
                }
            };

            // Используем using для ответа (HttpResponseMessage)
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                return body;
            }
        }
    }
}