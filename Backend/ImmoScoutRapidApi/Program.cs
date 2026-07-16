using Model;
using Api;

var connectionString = "Data Source=data.db";
var repository = new JsonDataRepository(connectionString);
var apiClient = new ImmoScoutApiClient();
var queryComposer = new QueryComposer();
var app = new App(repository, apiClient, queryComposer);
await app.RunAsync();
