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
        private readonly ITokenService _tokenService;

        public AuthService(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return new AuthResult { Success = false, ErrorMessage = "User already exists" };
            }

            var newUser = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new AuthResult { Success = true };
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return new LoginResult { Success = false, ErrorMessage = "User not found" };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return new LoginResult { Success = false, ErrorMessage = "Invalid password" };
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshTokenValue = _tokenService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
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

        public async Task<LoginResult> RefreshAsync(RefreshRequest request)
        {
            var existingToken = await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

            if (existingToken == null || existingToken.IsRevoked || existingToken.ExpiresAt < DateTime.UtcNow)
            {
                return new LoginResult { Success = false, ErrorMessage = "Invalid or expired refresh token" };
            }

            existingToken.IsRevoked = true;

            var accessToken = _tokenService.GenerateAccessToken(existingToken.User);
            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshTokenValue,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = existingToken.UserId
            });

            await _context.SaveChangesAsync();

            return new LoginResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = newRefreshTokenValue
            };
        }
    }
}
