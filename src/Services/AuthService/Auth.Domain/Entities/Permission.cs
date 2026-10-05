using SharedKernel.Attributes;
using SharedKernel.Entities;
using SharedKernel.Enums;

namespace Domain.Entities;

[Title("Разрешение")]
public sealed class Permission : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ObjectType { get; set; }
    public ActionType ActionType { get; set; }
    public ObjectType ObjectTypeRef { get; set; }
    public List<RolePermission> RolePermissions { get; set; } = [];
}
 