using SharedKernel.Contracts.Access;
using SharedKernel.Enums;

namespace SharedKernel.Access
{
    /// <summary>
    /// Единственное место, где задан формат ключа права в кэше: "CoreService:Lesson:Read:Related".
    /// AuthService пишет ключи этим форматом, остальные сервисы проверяют им же.
    /// </summary>
    public static class PermissionKey
    {
        public static string Format(ServiceType service, string objectType, ActionType action, AccessScope scope) =>
            $"{Base(service, objectType, action)}:{scope}";

        /// <summary>Ключ без области действия — по нему ищутся все области права.</summary>
        public static string Base(ServiceType service, string objectType, ActionType action) =>
            $"{service}:{objectType}:{action}";

        public static string WithScope(string baseKey, AccessScope scope) => $"{baseKey}:{scope}";
    }
}
