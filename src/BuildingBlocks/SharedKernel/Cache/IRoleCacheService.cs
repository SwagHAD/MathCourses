namespace SharedKernel.Cache
{
    public interface IRoleCacheService
    {
        Task SetUserRolesAsync(int userId, IEnumerable<int> roles, CancellationToken ct = default);
        Task<IReadOnlySet<int>> GetCurrentUserRolesIdAsync(CancellationToken ct = default);
        Task RemoveUserRolesAsync(int userId, CancellationToken ct = default);
        Task<bool> HasRoleAsync(int roleId, CancellationToken ct = default);
    }
}
