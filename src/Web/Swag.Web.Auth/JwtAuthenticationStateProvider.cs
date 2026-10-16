using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Swag.Web.Auth
{
    /// <summary>
    /// Пользователь считается вошедшим, пока есть пара токенов. Протухший access-токен не разлогинивает:
    /// его обновит <see cref="AuthorizedHttpHandler"/> при первом же запросе.
    /// </summary>
    public sealed class JwtAuthenticationStateProvider(TokenStorage storage) : AuthenticationStateProvider
    {
        private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokens = await storage.GetAsync();
            if (tokens is null)
                return Anonymous;
            try
            {
                var claims = JwtParser.ReadClaims(tokens.AccessToken);
                if (tokens.Login is not null)
                    claims.Add(new Claim(ClaimTypes.Name, tokens.Login));
                return new AuthenticationState(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Role)));
            }
            catch (Exception ex) when (ex is FormatException or System.Text.Json.JsonException)
            {
                return Anonymous;
            }
        }

        public void NotifyChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
