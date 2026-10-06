using SharedKernel.Enums;

namespace SharedKernel.Attributes
{
    public sealed class PermissionAttribute : Attribute
    {
        public string ObjectTypeName { get; set; } = null!;
        public ActionType ActionType { get; set; }
        public HashSet<RoleType> Roles { get; set; } = [];
    }
}
