using System.Net;

namespace Swag.Web.Api
{
    /// <summary>Неуспешный ответ сервиса с текстом ошибки из тела ({ error } или { errors: [] }).</summary>
    public sealed class ApiException(HttpStatusCode statusCode, string message)
        : HttpRequestException(message, null, statusCode);
}
