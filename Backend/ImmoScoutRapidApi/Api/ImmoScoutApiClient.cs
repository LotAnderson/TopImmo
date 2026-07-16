using System.Net.Http;
using System.Threading.Tasks;

namespace Api
{
    public class ImmoScoutApiClient
    {
        private readonly HttpClient _client;
        private const string ApiKey = "28ae1a1774mshc94195d2857a99dp1072a9jsn6fd839dee158";
        private const string ApiHost = "immoscout24-api.p.rapidapi.com";

        public ImmoScoutApiClient(HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
        }

        public async Task<string> GetApartmentsAsync(string url)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "x-rapidapi-key", ApiKey },
                    { "x-rapidapi-host", ApiHost },
                },
            };

            using var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}