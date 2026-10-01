namespace Application.Interfaces
{
    public interface IPermissionCache
    {
        Task SetUserPermissionsAsync(int userId, IEnumerable<string> permissions, CancellationToken ct = default);
        Task<IReadOnlySet<string>> GetUserPermissionsAsync(int userId, CancellationToken ct = default);
        Task RemoveUserPermissionsAsync(int userId, CancellationToken ct = default);
        Task<bool> HasPermissionAsync(int userId, string permission, CancellationToken ct = default);
    }
}
