namespace ImmscoutAPI.Model.Authorization
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;      // сама строка токена (случайная, длинная)
        public DateTime ExpiresAt { get; set; }                 // когда токен "протухнет"
        public bool IsRevoked { get; set; } = false;             // на будущее — можно ли токен отозвать вручную (logout)?

        public int UserId { get; set; }                          // связь с User
        public User User { get; set; } = null!;
    }
}
