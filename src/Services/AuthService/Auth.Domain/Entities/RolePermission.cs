using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities;

[Title("Роль-разрешение")]
public sealed class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public Role RoleRef { get; set; }

    public int PermissionId { get; set; }
    public Permission PermissionRef { get; set; }
}
