using System.Net;
using System.Net.Http.Json;
using AuthService.Contracts;
using AuthService.Contracts.Auth;
using Swag.Web.Api;

namespace Swag.Web.Auth
{
    /// <summary>
    /// Вход, выход и обновление пары токенов.
    /// Refresh-токен на сервере одноразовый (при обновлении старый отзывается), поэтому обновление
    /// идёт строго по одному: параллельные запросы, получившие 401, ждут первого и берут его результат.
    /// Регистрируется синглтоном — IHttpClientFactory создаёт обработчики в своём scope.
    /// </summary>
    public sealed class AuthSession(IHttpClientFactory httpClientFactory, TokenStorage storage,
        JwtAuthenticationStateProvider stateProvider)
    {
        /// <summary>Обновляем заранее, чтобы токен не истёк, пока запрос идёт до сервера.</summary>
        private static readonly TimeSpan RefreshBeforeExpiry = TimeSpan.FromSeconds(30);
        private readonly SemaphoreSlim _refreshLock = new(1, 1);

        /// <returns>null при успехе, иначе текст ошибки.</returns>
        public async Task<string?> LoginAsync(string login, string password, CancellationToken cancellationToken = default)
        {
            var client = httpClientFactory.CreateClient(HttpClients.AuthAnonymous);
            using var response = await client.PostAsJsonAsync(AuthRoutes.Url(AuthRoutes.Auth.Login),
                new LoginRequest { Login = login, Password = password }, cancellationToken);
            if (!response.IsSuccessStatusCode)
                return response.StatusCode == HttpStatusCode.Unauthorized
                    ? "Неверный логин или пароль"
                    : await ApiError.ReadAsync(response, cancellationToken);

            var tokens = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
            await SaveAsync(ToStored(tokens!, login));
            return null;
        }

        public async Task LogoutAsync(CancellationToken cancellationToken = default)
        {
            var tokens = await storage.GetAsync();
            if (tokens is not null)
            {
                try
                {
                    // Через клиент с токеном: logout на сервере требует авторизации
                    var client = httpClientFactory.CreateClient(HttpClients.Auth);
                    using var _ = await client.PostAsJsonAsync(AuthRoutes.Url(AuthRoutes.Auth.Logout),
                        new LogoutRequest { RefreshToken = tokens.RefreshToken }, cancellationToken);
                }
                catch (HttpRequestException)
                {
                    // Сервер недоступен — всё равно выходим локально
                }
            }
            await ClearAsync();
        }

        /// <summary>Действующий access-токен; если он вот-вот истечёт — сначала обновляет.</summary>
        public async Task<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            var tokens = await storage.GetAsync();
            if (tokens is null)
                return null;
            if (JwtParser.GetExpiry(tokens.AccessToken) - RefreshBeforeExpiry > DateTimeOffset.UtcNow)
                return tokens.AccessToken;
            return await RefreshAsync(tokens.AccessToken, cancellationToken);
        }

        /// <summary>
        /// Обновляет пару токенов, если текущий access-токен всё ещё <paramref name="staleAccessToken"/>.
        /// Если его уже обновил другой запрос, просто возвращает новый.
        /// </summary>
        /// <returns>Новый access-токен или null, если сессия закончилась и нужно войти заново.</returns>
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

                var client = httpClientFactory.CreateClient(HttpClients.AuthAnonymous);
                using var response = await client.PostAsJsonAsync(AuthRoutes.Url(AuthRoutes.Auth.Refresh),
                    new RefreshTokenRequest { RefreshToken = tokens.RefreshToken }, cancellationToken);

                if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.BadRequest)
                {
                    // Refresh-токен отозван или истёк — сессии больше нет
                    await ClearAsync();
                    return null;
                }
                // Сбой сервера или сети не повод разлогинивать: токены сохраняем, ошибка уйдёт вызывающему
                response.EnsureSuccessStatusCode();

                var refreshed = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
                await SaveAsync(ToStored(refreshed!, tokens.Login));
                return refreshed!.AccessToken;
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
