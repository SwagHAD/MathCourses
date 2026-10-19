using System.Net.Http.Json;

namespace Swag.Web.Api.Services.Internal
{
    /// <summary>Общие HTTP-вызовы typed-клиентов: неуспешный ответ превращается в <see cref="ApiException"/>.</summary>
    internal abstract class ApiClientBase(HttpClient httpClient)
    {
        protected HttpClient Http => httpClient;

        protected async Task<T> GetJsonAsync<T>(string url, CancellationToken cancellationToken)
        {
            using var response = await Http.GetAsync(url, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
            return (await response.Content.ReadFromJsonAsync<T>(cancellationToken))!;
        }

        protected async Task PostJsonAsync<TBody>(string url, TBody body, CancellationToken cancellationToken)
        {
            using var response = await Http.PostAsJsonAsync(url, body, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
        }

        protected async Task<TResult> PostJsonAsync<TBody, TResult>(string url, TBody body, CancellationToken cancellationToken)
        {
            using var response = await Http.PostAsJsonAsync(url, body, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
            return (await response.Content.ReadFromJsonAsync<TResult>(cancellationToken))!;
        }

        protected async Task PutJsonAsync<TBody>(string url, TBody body, CancellationToken cancellationToken)
        {
            using var response = await Http.PutAsJsonAsync(url, body, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
        }

        protected async Task<TResult> PutJsonAsync<TBody, TResult>(string url, TBody body, CancellationToken cancellationToken)
        {
            using var response = await Http.PutAsJsonAsync(url, body, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
            return (await response.Content.ReadFromJsonAsync<TResult>(cancellationToken))!;
        }

        protected async Task SendDeleteAsync(string url, CancellationToken cancellationToken)
        {
            using var response = await Http.DeleteAsync(url, cancellationToken);
            await EnsureSuccessAsync(response, cancellationToken);
        }

        protected static string WithId(string url, int id) => $"{url}?id={id}";

        protected static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, await ApiError.ReadAsync(response, cancellationToken));
        }
    }
}
