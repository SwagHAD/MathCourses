namespace Application.Responses.DefaultResponses
{
    public sealed record RoleResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int[] PermissionIds { get; init; } = [];
    }
}
