using System.Net.Http.Headers;
using System.Net.Http.Json;
using AuthService.Contracts;
using AuthService.Contracts.Auth;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class AuthApi(HttpClient httpClient) : ApiClientBase(httpClient), IAuthApi
    {
        public Task<TokenResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default) =>
            PostJsonAsync<LoginRequest, TokenResponse>(AuthRoutes.Url(AuthRoutes.Auth.Login), request, cancellationToken);

        public Task<TokenResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default) =>
            PostJsonAsync<RefreshTokenRequest, TokenResponse>(AuthRoutes.Url(AuthRoutes.Auth.Refresh), request, cancellationToken);

        public async Task LogoutAsync(LogoutRequest request, string? accessToken, CancellationToken cancellationToken = default)
        {
            using var message = new HttpRequestMessage(HttpMethod.Post, AuthRoutes.Url(AuthRoutes.Auth.Logout))
            {
                Content = JsonContent.Create(request)
            };
            if (accessToken is not null)
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            using var response = await Http.SendAsync(message, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
        }
    }
}
