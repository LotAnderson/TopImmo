using Microsoft.Data.Sqlite;

namespace Model
{
    public class JsonDataRepository
    {
        private readonly SqliteConnection _connection;

        public JsonDataRepository(string connectionString)
        {
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            CreateTableIfNotExists();
        }

        private void CreateTableIfNotExists()
        {
            using var createTableCmd = _connection.CreateCommand();
            createTableCmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS JsonData (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            City TEXT NOT NULL,
            Body TEXT NOT NULL,
            HouseUrl TEXT
        );";
            createTableCmd.ExecuteNonQuery();

            // Try to add the HouseUrl column if it doesn't exist
            try
            {
                using var alterCmd = _connection.CreateCommand();
                alterCmd.CommandText = "ALTER TABLE JsonData ADD COLUMN HouseUrl TEXT;";
                alterCmd.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                if (!ex.Message.Contains("duplicate column name"))
                    throw;
            }
        }


        public int Insert(string city, string body, string houseUrl)
        {
            using var insertCmd = _connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO JsonData (City, Body, HouseUrl) VALUES ($city, $body, $houseUrl);";
            insertCmd.Parameters.AddWithValue("$city", city);
            insertCmd.Parameters.AddWithValue("$body", body);
            insertCmd.Parameters.AddWithValue("$houseUrl", houseUrl);
            insertCmd.ExecuteNonQuery();

            insertCmd.CommandText = "SELECT last_insert_rowid();";
            var id = (long)insertCmd.ExecuteScalar()!;
            return (int)id;
        }


        public IEnumerable<(int Id, string City, string Body)> GetByCity(string city)
        {
            using var selectCmd = _connection.CreateCommand();
            selectCmd.CommandText = "SELECT Id, City, Body FROM JsonData WHERE City = $city;";
            selectCmd.Parameters.AddWithValue("$city", city);
            using var reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                yield return (reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }
        }
    }
}