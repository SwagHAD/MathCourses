namespace Swag.Web.Api.Services.Internal
{
    /// <summary>Полные URL эндпоинтов одной сущности. Пустые — у сущности такой операции нет.</summary>
    internal sealed record EntityRoutes(string Get, string GetPage, string Create = "", string Update = "", string Delete = "");
}
