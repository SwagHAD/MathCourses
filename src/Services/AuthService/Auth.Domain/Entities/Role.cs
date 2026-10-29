using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities;

[Title("Роль", Secured = true)]
public sealed class Role : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public Role ParentRoleRef { get; set; }
    public List<RolePermission> RolePermissions { get; set; } = [];
}
