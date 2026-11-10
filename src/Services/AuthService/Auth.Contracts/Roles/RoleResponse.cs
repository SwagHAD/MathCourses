namespace AuthService.Contracts.Roles
{
    public sealed record RoleResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public IReadOnlyList<RolePermissionGrant> Permissions { get; init; } = [];
    }
}
