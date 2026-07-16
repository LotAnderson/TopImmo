using ImmscoutAPI.DTO;
using ImmscoutAPI.Model;
using ImmscoutAPI.Model.Authorization;

namespace ImmscoutAPI.Interface
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterRequest request);
        Task<LoginResult> LoginAsync(LoginRequest request);
        Task<LoginResult> RefreshAsync(RefreshRequest request);
    }
}
