namespace ImmscoutAPI.Model
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public int ExpireMinutes { get; set; }
    }
}
