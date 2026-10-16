namespace AuthService.Contracts.Roles
{
    /// <summary>Список доступов заменяется целиком.</summary>
    public sealed record UpdateRoleRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public IReadOnlyList<int> PermissionIds { get; init; } = [];
    }
}
