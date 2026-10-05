using ImmscoutAPI.Model.Authorization;

namespace ImmscoutAPI.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
