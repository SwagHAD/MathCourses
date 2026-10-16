namespace AuthService.Contracts.Roles
{
    public sealed record CreateRoleRequest
    {
        public string Name { get; init; } = null!;
        public IReadOnlyList<int> PermissionIds { get; init; } = [];
    }
}
