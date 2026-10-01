using Domain.Enums;

namespace Domain.Attributes
{
    public sealed class PermissionAttribute : Attribute
    {
        public string ObjectTypeName { get; set; } = null!;
        public ActionType ActionType { get; set; }
        public RoleType? RoleType { get; set; }
    }
}
