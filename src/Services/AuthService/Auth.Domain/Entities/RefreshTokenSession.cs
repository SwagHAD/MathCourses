using Domain.Attributes;
using Domain.Base;

namespace Domain.Entities;

[Title("Сессия refresh-токена")]
public sealed class RefreshTokenSession : BaseEntity
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User UserRef { get; set; } = null!;
    public string TokenHash { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
}
