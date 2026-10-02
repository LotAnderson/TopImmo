namespace ImmscoutAPI.Model.Authorization
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;   // не пароль! А что тут хранится вместо него?

        // навигационное свойство — один пользователь может иметь много refresh-токенов
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }
}
