using Model;
using Api;

var builder = WebApplication.CreateBuilder(args);

// Register services for dependency injection
builder.Services.AddControllers();
builder.Services.AddSingleton<UserLoginService>();
builder.Services.AddSingleton<JsonDataRepository>(sp =>
    new JsonDataRepository("Data Source=data.db"));
builder.Services.AddSingleton<ImmoScoutApiClient>();
builder.Services.AddSingleton<QueryComposer>();

var app = builder.Build();

// Map controller endpoints
app.MapControllers();

app.MapGet("/", () => "ImmoScoutRapidApi is running.");
app.Run();
