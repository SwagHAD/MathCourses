namespace SharedKernel.Cache
{
    public interface IPermissionCacheService
    {
        Task SetUserPermissionsAsync(int userId, IEnumerable<string> permissions, CancellationToken ct = default);
        Task<IReadOnlySet<string>> GetCurrentUserPermissionsAsync(CancellationToken ct = default);
        Task RemoveUserPermissionsAsync(int userId, CancellationToken ct = default);
        Task<bool> HasPermissionAsync(string permission, CancellationToken ct = default);
    }
}
