namespace AuthService.Contracts.Permissions
{
    public sealed record PermissionResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
