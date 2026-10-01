namespace Application.Responses.DefaultResponses
{
    public sealed record UserResponse
    {
        public int Id { get; init; }
        public string Login { get; init; } = null!;
        public int Role { get; init; }
    }
}
