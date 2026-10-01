namespace Application.Responses
{
    public sealed record RefreshResponse
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
