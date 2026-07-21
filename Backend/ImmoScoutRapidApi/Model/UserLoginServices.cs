using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class UserLoginService
    {
        private readonly Dictionary<string, string> _users = new()
        {
            { "user1", "password1" },
            { "admin", "adminpass" }
        };

        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            return _users.TryGetValue(username, out var storedPassword) && storedPassword == password;
        }

        // Generates a secure random session ID and returns its SHA256 hash as a hex string
        public string GenerateSessionId()
        {
            // Generate 32 random bytes
            byte[] randomBytes = new byte[32];
            RandomNumberGenerator.Fill(randomBytes);

            // Hash the random bytes using SHA256
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(randomBytes);

            // Convert hash to hex string
            var sb = new StringBuilder(hashBytes.Length * 2);
            foreach (var b in hashBytes)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }
    }
}