using Microsoft.Data.Sqlite;

var connectionString = "Data Source=data.db";
using var connection = new SqliteConnection(connectionString);
connection.Open();

var createTableCmd = connection.CreateCommand();
createTableCmd.CommandText = @"
CREATE TABLE IF NOT EXISTS JsonData (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Body TEXT NOT NULL
);";
createTableCmd.ExecuteNonQuery();

Console.WriteLine("Table created (if it did not exist).");