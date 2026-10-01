namespace Application.Responses
{
    public sealed record LoginResponse
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
