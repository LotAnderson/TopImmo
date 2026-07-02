using ImmscoutAPI.DataBase;
using ImmscoutAPI.DTO;
using ImmscoutAPI.Interface;
using ImmscoutAPI.Model;
using ImmscoutAPI.Model.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ImmscoutAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
        }
        public async Task<AuthResult> RegisterAsync(DTO.RegisterRequest request)
        {
            // Check if the user already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return new AuthResult { Success = false, ErrorMessage = "User already exists" };
            }
            // Create a new user
            var newUser = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) // Hash the password
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return new AuthResult { Success = true };
        }
        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            // 1. Найти пользователя
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return new LoginResult { Success = false, ErrorMessage = "User not found" };
            }

            // 2. Проверить пароль — БЕЗ расшифровки, через специальный метод BCrypt для сравнения
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return new LoginResult { Success = false, ErrorMessage = "Invalid password" };
            }

            // 3. Генерируем токены
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshTokenValue = _tokenService.GenerateRefreshToken();

            // 4. Сохраняем refresh token в базу
            var refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60),   
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return new LoginResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue
            };
        }
    }
}
