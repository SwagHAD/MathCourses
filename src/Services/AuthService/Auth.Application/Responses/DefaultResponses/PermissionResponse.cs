namespace Application.Responses.DefaultResponses
{
    public sealed record PermissionResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; set; }
    }
}
