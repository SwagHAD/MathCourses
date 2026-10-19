namespace Swag.Web.Api
{
    /// <summary>
    /// Адреса микросервисов. Сейчас напрямую на сервисы; чтобы ходить через gateway,
    /// укажите в обоих http://localhost:8080/ (nginx проксирует /api/auth и /api/core).
    /// </summary>
    public sealed class ApiOptions
    {
        public const string SectionName = "Api";

        public string AuthBaseUrl { get; set; } = null!;
        public string CoreBaseUrl { get; set; } = null!;
    }
}
