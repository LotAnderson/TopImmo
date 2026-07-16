namespace Api
{
    public class QueryComposer
    {
        public string ComposeUrl(string city)
        {
            // You can add more parameters as needed
            var url = $"https://immoscout24-api.p.rapidapi.com/v1/search?realEstateType=apartmentrent&page=1&priceType=calculatedtotalrent&pageSize=25&location={Uri.EscapeDataString(city)}&country=de&sort=standard";
            return url;
        }
    }
}
