using AuthService.Contracts.Auth;

namespace Swag.Web.Api.Services.Interfaces
{
    /// <summary>
    /// Вход, обновление и выход. Клиент без обработчика токенов — иначе refresh зациклился бы сам на себя;
    /// access-токен для выхода передаётся явно.
    /// </summary>
    public interface IAuthApi
    {
        Task<TokenResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
        Task<TokenResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
        Task LogoutAsync(LogoutRequest request, string? accessToken, CancellationToken cancellationToken = default);
    }
}
