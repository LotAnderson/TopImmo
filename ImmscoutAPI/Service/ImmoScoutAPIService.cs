using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImmscoutAPI.Service
{
    public class ImmoScoutAPIService
    {
        // Создаем статический или используем фабрику HttpClient во избежание истощения сокетов
        private readonly HttpClient _client;

        public ImmoScoutAPIService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetStuttgartApartmentsAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://immoscout24-api.p.rapidapi.com/v1/search?realEstateType=apartmentrent&page=1&priceType=calculatedtotalrent&pageSize=25&location=Stuttgart&country=de&sort=standard"),
                Headers =
                {
                    { "x-rapidapi-key", "7c4a1401e7mshd4d0437308c4470p139010jsndfbd419e6606" },
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