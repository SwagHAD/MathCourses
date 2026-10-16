namespace AuthService.Contracts.Auth
{
    public sealed record LogoutRequest
    {
        public string RefreshToken { get; init; } = null!;
    }
}
