using Model;
using Api;

var builder = WebApplication.CreateBuilder(args);

// Register services for dependency injection
builder.Services.AddControllers();
builder.Services.AddSingleton<UserLoginService>();
builder.Services.AddSingleton<JsonDataRepository>(sp =>
{
    var configuredConnection = builder.Configuration.GetConnectionString("Database");
    var connectionString = configuredConnection ??
        $"Data Source={Path.Combine(AppContext.BaseDirectory, "data.db")}";
    return new JsonDataRepository(connectionString);
});
builder.Services.AddSingleton<ImmoScoutApiClient>();
builder.Services.AddSingleton<QueryComposer>();

var app = builder.Build();

// Open the database now so its schema and duplicate cleanup run at startup.
app.Services.GetRequiredService<JsonDataRepository>();

// Map controller endpoints
app.MapControllers();

app.MapGet("/", () => "ImmoScoutRapidApi is running.");
app.Run();
