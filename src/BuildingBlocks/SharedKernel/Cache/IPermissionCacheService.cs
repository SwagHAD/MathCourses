using SharedKernel.Contracts.Access;

namespace SharedKernel.Cache
{
    /// <summary>
    /// Права текущего пользователя в кэше. Ключи в формате <see cref="Access.PermissionKey"/>:
    /// "Service:Object:Action:Scope".
    /// </summary>
    public interface IPermissionCacheService
    {
        Task SetUserPermissionsAsync(int userId, IEnumerable<string> permissions, CancellationToken ct = default);
        Task<IReadOnlySet<string>> GetCurrentUserPermissionsAsync(CancellationToken ct = default);
        Task RemoveUserPermissionsAsync(int userId, CancellationToken ct = default);
        /// <summary>Есть ли у текущего пользователя ключ права ровно в таком виде.</summary>
        Task<bool> HasPermissionAsync(string permission, CancellationToken ct = default);
        /// <summary>
        /// Самая широкая область, в которой у текущего пользователя есть право;
        /// null — права нет ни в какой области.
        /// </summary>
        /// <param name="basePermission">Ключ без области: <see cref="Access.PermissionKey.Base"/>.</param>
        Task<AccessScope?> GetScopeAsync(string basePermission, CancellationToken ct = default);
    }
}
