using SharedKernel.Enums;

namespace SharedKernel.Attributes
{
    public sealed class PermissionAttribute : Attribute
    {
        public string ObjectTypeName { get; set; } = null!;
        public ActionType ActionType { get; set; }
        /// <summary>Роли, которым доступен эндпоинт. Пусто — роль не проверяется.</summary>
        public RoleType[] Roles { get; set; } = [];
    }
}
