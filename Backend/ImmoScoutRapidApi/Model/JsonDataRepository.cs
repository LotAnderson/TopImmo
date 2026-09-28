using Microsoft.Data.Sqlite;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Model
{
    public class JsonDataRepository
    {
        private readonly SqliteConnection _connection;
        private readonly object _databaseLock = new();

        public JsonDataRepository(string connectionString)
        {
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            CreateTablesIfNotExist();
            RemoveDuplicateListings();
        }

        private void CreateTablesIfNotExist()
        {
            using var createTableCmd = _connection.CreateCommand();
            createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS JsonData (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    City TEXT NOT NULL,
                    Body TEXT NOT NULL,
                    HouseUrl TEXT
                );

                CREATE TABLE IF NOT EXISTS ListingIds (
                    ListingId TEXT PRIMARY KEY
                );";
            createTableCmd.ExecuteNonQuery();

            // Keep compatibility with databases created before HouseUrl was added.
            try
            {
                using var alterCmd = _connection.CreateCommand();
                alterCmd.CommandText = "ALTER TABLE JsonData ADD COLUMN HouseUrl TEXT;";
                alterCmd.ExecuteNonQuery();
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 1 &&
                                             ex.Message.Contains("duplicate column name", StringComparison.OrdinalIgnoreCase))
            {
            }
        }

        public int Insert(string city, string body, string houseUrl)
        {
            lock (_databaseLock)
            {
                using var transaction = _connection.BeginTransaction();
                var filteredBody = FilterNewListings(body, transaction, out var firstListingUrl);

                using var insertCmd = _connection.CreateCommand();
                insertCmd.Transaction = transaction;
                insertCmd.CommandText = @"
                    INSERT INTO JsonData (City, Body, HouseUrl)
                    VALUES ($city, $body, $houseUrl);
                    SELECT last_insert_rowid();";
                insertCmd.Parameters.AddWithValue("$city", city);
                insertCmd.Parameters.AddWithValue("$body", filteredBody);
                insertCmd.Parameters.AddWithValue("$houseUrl", firstListingUrl);

                var id = (long)insertCmd.ExecuteScalar()!;
                transaction.Commit();
                return checked((int)id);
            }
        }

        /// <summary>
        /// Removes repeated listings from stored responses, keeping their first occurrence.
        /// The listing ID index is rebuilt at the same time so future inserts are deduplicated.
        /// </summary>
        public int RemoveDuplicateListings()
        {
            lock (_databaseLock)
            {
                var rows = new List<(int Id, string Body)>();
                using (var selectCmd = _connection.CreateCommand())
                {
                    selectCmd.CommandText = "SELECT Id, Body FROM JsonData ORDER BY Id;";
                    using var reader = selectCmd.ExecuteReader();
                    while (reader.Read())
                        rows.Add((reader.GetInt32(0), reader.GetString(1)));
                }

                using var transaction = _connection.BeginTransaction();
                using (var clearCmd = _connection.CreateCommand())
                {
                    clearCmd.Transaction = transaction;
                    clearCmd.CommandText = "DELETE FROM ListingIds;";
                    clearCmd.ExecuteNonQuery();
                }

                var removedCount = 0;
                foreach (var row in rows)
                {
                    var filteredBody = FilterNewListings(
                        row.Body,
                        transaction,
                        out var firstListingUrl,
                        out var removedFromRow);

                    removedCount += removedFromRow;
                    using var updateCmd = _connection.CreateCommand();
                    updateCmd.Transaction = transaction;
                    updateCmd.CommandText = @"
                        UPDATE JsonData
                        SET Body = $body, HouseUrl = $houseUrl
                        WHERE Id = $id;";
                    updateCmd.Parameters.AddWithValue("$body", filteredBody);
                    updateCmd.Parameters.AddWithValue("$houseUrl", firstListingUrl);
                    updateCmd.Parameters.AddWithValue("$id", row.Id);
                    updateCmd.ExecuteNonQuery();
                }

                transaction.Commit();
                return removedCount;
            }
        }

        public IEnumerable<(int Id, string City, string Body)> GetByCity(string city)
        {
            var results = new List<(int Id, string City, string Body)>();
            lock (_databaseLock)
            {
                using var selectCmd = _connection.CreateCommand();
                selectCmd.CommandText = "SELECT Id, City, Body FROM JsonData WHERE City = $city;";
                selectCmd.Parameters.AddWithValue("$city", city);
                using var reader = selectCmd.ExecuteReader();
                while (reader.Read())
                    results.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }

            return results;
        }

        private string FilterNewListings(
            string body,
            SqliteTransaction transaction,
            out string firstListingUrl)
        {
            return FilterNewListings(body, transaction, out firstListingUrl, out _);
        }

        private string FilterNewListings(
            string body,
            SqliteTransaction transaction,
            out string firstListingUrl,
            out int removedCount)
        {
            firstListingUrl = string.Empty;
            removedCount = 0;

            JsonNode? root;
            try
            {
                root = JsonNode.Parse(body);
            }
            catch (JsonException)
            {
                // Keep an unexpected API response intact instead of losing data.
                return body;
            }

            if (root is not JsonObject rootObject || rootObject["listings"] is not JsonArray listings)
                return body;

            var uniqueListings = new JsonArray();
            foreach (var listing in listings)
            {
                var listingId = listing?["id"]?.GetValue<string>();
                if (!string.IsNullOrWhiteSpace(listingId) && !TryRegisterListingId(listingId, transaction))
                {
                    removedCount++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(firstListingUrl))
                    firstListingUrl = listing?["url"]?.GetValue<string>() ?? string.Empty;

                uniqueListings.Add(listing?.DeepClone());
            }

            rootObject["listings"] = uniqueListings;
            return rootObject.ToJsonString(new JsonSerializerOptions { WriteIndented = false });
        }

        private bool TryRegisterListingId(string listingId, SqliteTransaction transaction)
        {
            using var insertIdCmd = _connection.CreateCommand();
            insertIdCmd.Transaction = transaction;
            insertIdCmd.CommandText = "INSERT OR IGNORE INTO ListingIds (ListingId) VALUES ($listingId);";
            insertIdCmd.Parameters.AddWithValue("$listingId", listingId);
            return insertIdCmd.ExecuteNonQuery() == 1;
        }
    }
}
