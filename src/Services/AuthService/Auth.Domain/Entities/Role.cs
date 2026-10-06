using SharedKernel.Attributes;
using SharedKernel.Entities;
using SharedKernel.Enums;

namespace Domain.Entities;

[Title("Роль", Secured = true)]
public sealed class Role : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? NormalizedName { get; set; }
    public RoleType UserType { get; set; }
    public List<RolePermission> RolePermissions { get; set; } = [];
}
