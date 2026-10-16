namespace AuthService.Contracts.Auth
{
    public sealed record LogoutResponse
    {
        public bool Success { get; init; }
    }
}
