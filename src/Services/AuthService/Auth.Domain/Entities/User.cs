using SharedKernel.Attributes;
using SharedKernel.Entities;
using Domain.Enums;

namespace Domain.Entities;

[Title("Пользователь")]
public sealed class  User : BaseEntity
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserStatus Status { get; set; }
    public int? RoleId { get; set; }
    public Role RoleRef { get; set; }
    public List<RefreshTokenSession> RefreshSessions { get; set; } = [];
}
