using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Swag.Web.Api
{
    /// <summary>Достаёт текст ошибки из ответов ErrorHandlingMiddleWare: { error } или { errors: [] }.</summary>
    public static class ApiError
    {
        public static async Task<string> ReadAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            try
            {
                var body = await response.Content.ReadFromJsonAsync<JsonElement>(cancellationToken);
                if (body.ValueKind == JsonValueKind.Object)
                {
                    if (body.TryGetProperty("error", out var error) && error.ValueKind == JsonValueKind.String)
                        return error.GetString()!;
                    if (body.TryGetProperty("errors", out var errors) && errors.ValueKind == JsonValueKind.Array)
                        return string.Join(Environment.NewLine, errors.EnumerateArray().Select(e => e.ToString()));
                }
            }
            catch (JsonException) { }
            catch (NotSupportedException) { }
            return Describe(response.StatusCode);
        }

        public static string Describe(Exception exception) => exception switch
        {
            HttpRequestException { StatusCode: { } status } => Describe(status),
            HttpRequestException => "Сервер недоступен",
            _ => exception.Message
        };

        public static string Describe(HttpStatusCode status) => status switch
        {
            HttpStatusCode.Unauthorized => "Сессия истекла, войдите заново",
            HttpStatusCode.Forbidden => "Нет доступа",
            HttpStatusCode.NotFound => "Не найдено",
            _ => $"Ошибка сервера ({(int)status})"
        };
    }
}
