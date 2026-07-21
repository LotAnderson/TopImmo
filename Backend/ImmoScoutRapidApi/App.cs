using Model;
using Api;
using System.Text.Json;
using System;

public class App
{
    private readonly JsonDataRepository _repository;
    private readonly ImmoScoutApiClient _apiClient;
    private readonly QueryComposer _queryComposer;
    private readonly UserLoginService _loginService;

    public App(JsonDataRepository repository, ImmoScoutApiClient apiClient, QueryComposer queryComposer)
    {
        _repository = repository;
        _apiClient = apiClient;
        _queryComposer = queryComposer;
        _loginService = new UserLoginService();
    }

    private bool Login()
    {
        Console.Write("Username: ");
        var username = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();

        bool isValid = _loginService.ValidateCredentials(username, password);
        if (isValid)
        {
            Console.WriteLine("Login successful!");
            string sid = _loginService.GenerateSessionId();
            Console.WriteLine($"Session ID: {sid}");
            return true;
        }
        else
        {
            Console.WriteLine("Invalid credentials.");
            return false;
        }
    }

    public async Task RunAsync()
    {
        // Require login before proceeding
        if (!Login())
            return;

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

    private string ExtractFirstHouseUrl(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (root.TryGetProperty("units", out var units) && units.ValueKind == JsonValueKind.Array && units.GetArrayLength() > 0)
        {
            var firstUnit = units[0];
            if (firstUnit.TryGetProperty("url", out var urlProp))
            {
                return urlProp.GetString() ?? "";
            }
        }
        return "";
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
        var houseUrl = ExtractFirstHouseUrl(body);
        int newId = _repository.Insert(city, body, houseUrl);
        Console.WriteLine($"Response saved to database with Id: {newId}, HouseUrl: {houseUrl}");
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
}
