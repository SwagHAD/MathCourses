using SharedKernel.Enums;

namespace Application.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateAccessToken(int userId, RoleType userType);
        string GenerateRefreshToken();
    }
}
