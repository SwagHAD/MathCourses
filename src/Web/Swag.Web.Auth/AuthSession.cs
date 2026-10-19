using System.Net;
using AuthService.Contracts.Auth;
using Swag.Web.Api;
using Swag.Web.Api.Services.Interfaces;
using Swag.Web.Auth.Interfaces;

namespace Swag.Web.Auth
{
    /// <summary>
    /// Вход, выход и обновление пары токенов.
    /// Refresh-токен на сервере одноразовый (при обновлении старый отзывается), поэтому обновление
    /// идёт строго по одному: параллельные запросы, получившие 401, ждут первого и берут его результат.
    /// Синглтон: блокировка и уведомления о входе общие на всё приложение. Держит typed-клиент
    /// <see cref="IAuthApi"/> всё время жизни — в WASM ротация обработчиков HttpClient не нужна.
    /// </summary>
    internal sealed class AuthSession(IAuthApi authApi, ITokenStorage storage,
        JwtAuthenticationStateProvider stateProvider) : IAuthSession
    {
        /// <summary>Обновляем заранее, чтобы токен не истёк, пока запрос идёт до сервера.</summary>
        private static readonly TimeSpan RefreshBeforeExpiry = TimeSpan.FromSeconds(30);
        private readonly SemaphoreSlim _refreshLock = new(1, 1);

        public async Task<string?> LoginAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var tokens = await authApi.LoginAsync(new LoginRequest { Login = login, Password = password }, cancellationToken);
                await SaveAsync(ToStored(tokens, login));
                return null;
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return "Неверный логин или пароль";
            }
            catch (ApiException ex)
            {
                return ApiError.Describe(ex);
            }
        }

        public async Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            var tokens = await storage.GetAsync();
            if (tokens is not null)
            {
                try
                {
                    // Logout на сервере требует авторизации — берём действующий, при необходимости обновлённый токен
                    var accessToken = await GetAccessTokenAsync(cancellationToken);
                    var current = await storage.GetAsync() ?? tokens;
                    await authApi.LogoutAsync(new LogoutRequest { RefreshToken = current.RefreshToken }, accessToken, cancellationToken);
                }
                catch (HttpRequestException)
                {
                    // Сервер недоступен или сессия уже отозвана — всё равно выходим локально
                }
            }
            await ClearAsync();
        }

        public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            var tokens = await storage.GetAsync();
            if (tokens is null)
                return null;
            if (JwtParser.GetExpiry(tokens.AccessToken) - RefreshBeforeExpiry > DateTimeOffset.UtcNow)
                return tokens.AccessToken;
            return await RefreshAsync(tokens.AccessToken, cancellationToken);
        }

        public async Task<string?> RefreshAsync(string staleAccessToken, CancellationToken cancellationToken = default)
        {
            await _refreshLock.WaitAsync(cancellationToken);
            try
            {
                var tokens = await storage.GetAsync();
                if (tokens is null)
                    return null;
                if (tokens.AccessToken != staleAccessToken)
                    return tokens.AccessToken;

                TokenResponse refreshed;
                try
                {
                    refreshed = await authApi.RefreshAsync(new RefreshTokenRequest { RefreshToken = tokens.RefreshToken },
                        cancellationToken);
                }
                catch (ApiException ex) when (ex.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.BadRequest)
                {
                    // Refresh-токен отозван или истёк — сессии больше нет.
                    // Сбой сервера или сети не повод разлогинивать: такие ошибки уходят вызывающему.
                    await ClearAsync();
                    return null;
                }

                await SaveAsync(ToStored(refreshed, tokens.Login));
                return refreshed.AccessToken;
            }
            finally
            {
                _refreshLock.Release();
            }
        }

        private static AuthTokens ToStored(TokenResponse tokens, string? login) => new()
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            Login = login
        };

        private async Task SaveAsync(AuthTokens tokens)
        {
            await storage.SetAsync(tokens);
            stateProvider.NotifyChanged();
        }

        private async Task ClearAsync()
        {
            await storage.ClearAsync();
            stateProvider.NotifyChanged();
        }
    }
}
