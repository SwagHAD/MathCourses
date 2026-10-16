using System.Text.Json;
using Microsoft.JSInterop;

namespace Swag.Web.Auth
{
    /// <summary>
    /// Токены в localStorage: переживают перезагрузку страницы и общие для вкладок.
    /// Читаем каждый раз из хранилища, чтобы вкладка видела токены, обновлённые соседней вкладкой.
    /// </summary>
    public sealed class TokenStorage(IJSRuntime js)
    {
        private const string Key = "swag.auth";

        public async ValueTask<AuthTokens?> GetAsync()
        {
            var json = await js.InvokeAsync<string?>("localStorage.getItem", Key);
            if (string.IsNullOrEmpty(json))
                return null;
            try
            {
                return JsonSerializer.Deserialize<AuthTokens>(json);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public ValueTask SetAsync(AuthTokens tokens) =>
            js.InvokeVoidAsync("localStorage.setItem", Key, JsonSerializer.Serialize(tokens));

        public ValueTask ClearAsync() =>
            js.InvokeVoidAsync("localStorage.removeItem", Key);
    }
}
