using ImmscoutAPI.DTO;
using ImmscoutAPI.Interface;
using ImmscoutAPI.Model.Authorization;
using ImmscoutAPI.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImmscoutAPI.Controllers
{[ApiController]
[Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            if (!result.Success)
            {
                return Conflict(new { message = result.ErrorMessage });
            }

            return Ok(result);
        }
        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            // 1. Найти пользователя
            var user = await _context.Users.???;

            if (user == null)
            {
                return new LoginResult { Success = false, ErrorMessage = "??? " };
            }

            // 2. Проверить пароль — БЕЗ расшифровки, через специальный метод BCrypt для сравнения
            bool isPasswordValid = BCrypt.Net.BCrypt.??? (request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return new LoginResult { Success = false, ErrorMessage = "???" };
            }

            // 3. Генерируем токены
            var accessToken = _tokenService.??? (user);
            var refreshTokenValue = _tokenService.??? ();

            // 4. Сохраняем refresh token в базу
            var refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                ExpiresAt = ???,   // сколько дней/часов должен жить refresh token? (мы не задавали это в конфиге — где хранить это число?)
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.??? ();

            return new LoginResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue
            };
        }
    }
}
