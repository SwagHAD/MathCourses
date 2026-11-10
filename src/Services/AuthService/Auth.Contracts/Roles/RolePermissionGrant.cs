using SharedKernel.Contracts.Access;

namespace AuthService.Contracts.Roles
{
    /// <summary>Право, выданное роли, и область, на которую оно действует.</summary>
    public sealed record RolePermissionGrant
    {
        public int PermissionId { get; init; }
        public AccessScope Scope { get; init; }
    }
}
