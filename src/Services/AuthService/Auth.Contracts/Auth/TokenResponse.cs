namespace AuthService.Contracts.Auth
{
    /// <summary>Пара токенов — ответ и на вход, и на обновление. Refresh-токен одноразовый.</summary>
    public sealed record TokenResponse
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
