namespace AuthService.Contracts.Auth
{
    public sealed record LoginRequest
    {
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
