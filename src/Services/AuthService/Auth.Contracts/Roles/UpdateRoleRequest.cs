namespace AuthService.Contracts.Roles
{
    /// <summary>Список прав заменяется целиком.</summary>
    public sealed record UpdateRoleRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public IReadOnlyList<RolePermissionGrant> Permissions { get; init; } = [];
    }
}
