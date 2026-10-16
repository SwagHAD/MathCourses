namespace AuthService.Contracts.Auth
{
    public sealed record RefreshTokenRequest
    {
        public string RefreshToken { get; init; } = null!;
    }
}
