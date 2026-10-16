namespace Swag.Web.Auth
{
    public sealed record AuthTokens
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
        /// <summary>Логина нет в access-токене, запоминаем его при входе для отображения.</summary>
        public string? Login { get; init; }
    }
}
