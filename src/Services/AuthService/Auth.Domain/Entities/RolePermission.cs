using SharedKernel.Attributes;
using SharedKernel.Contracts.Access;
using SharedKernel.Entities;

namespace Domain.Entities;

/// <summary>Выдача права роли: само право (тип объекта × действие) и область, на которую оно действует.</summary>
[Title("Роль-разрешение")]
public sealed class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public Role RoleRef { get; set; }

    public int PermissionId { get; set; }
    public Permission PermissionRef { get; set; }

    public AccessScope Scope { get; set; }
}
