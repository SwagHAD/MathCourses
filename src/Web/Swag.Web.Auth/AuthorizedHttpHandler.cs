using System.Net;
using System.Net.Http.Headers;

namespace Swag.Web.Auth
{
    /// <summary>
    /// Подставляет Bearer-токен. На 401 один раз обновляет токены и повторяет запрос.
    /// Если обновить не удалось, отдаёт исходный 401: состояние уже сброшено,
    /// и AuthorizeRouteView сам отправит на страницу входа.
    /// </summary>
    public sealed class AuthorizedHttpHandler(AuthSession authSession) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await authSession.GetAccessTokenAsync(cancellationToken);
            if (accessToken is null)
                return await base.SendAsync(request, cancellationToken);

            // Буферизуем тело, чтобы его можно было отправить второй раз
            if (request.Content is not null)
                await request.Content.LoadIntoBufferAsync(cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return response;

            var refreshedToken = await authSession.RefreshAsync(accessToken, cancellationToken);
            if (refreshedToken is null)
                return response;

            response.Dispose();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshedToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
