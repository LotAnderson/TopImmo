using Model;
using Api;

public class App
{
    private readonly JsonDataRepository _repository;
    private readonly ImmoScoutApiClient _apiClient;
    private readonly QueryComposer _queryComposer;

    public App(JsonDataRepository repository, ImmoScoutApiClient apiClient, QueryComposer queryComposer)
    {
        _repository = repository;
        _apiClient = apiClient;
        _queryComposer = queryComposer;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("a) Get all infos from the DB for a city");
            Console.WriteLine("b) Send a new request");
            Console.WriteLine("c) Exit");
            Console.Write("Your choice: ");
            var choice = Console.ReadLine()?.Trim().ToLower();

            switch (choice)
            {
                case "a":
                    GetByCityFromDb();
                    break;
                case "b":
                    await RunRequestAndSaveAsync();
                    break;
                case "c":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose a, b, or c.");
                    break;
            }
        }
    }

    private void GetByCityFromDb()
    {
        Console.Write("Enter city to filter: ");
        var city = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("City cannot be empty.");
            return;
        }

        int count = 0;
        foreach (var (id, cityName, body) in _repository.GetByCity(city))
        {
            count++;
            Console.WriteLine($"\nId: {id}");
            Console.WriteLine($"City: {cityName}");
            Console.WriteLine($"Body: {body}");
        }
        if (count == 0)
        {
            Console.WriteLine("No entries found in the database for this city.");
        }
    }

    private async Task RunRequestAndSaveAsync()
    {
        Console.Write("Enter city: ");
        var city = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("City cannot be empty.");
            return;
        }

        var url = _queryComposer.ComposeUrl(city);
        var body = await _apiClient.GetApartmentsAsync(url);
        Console.WriteLine(body);

        int newId = _repository.Insert(city, body);
        Console.WriteLine($"Response saved to database with Id: {newId}");
    }
}
