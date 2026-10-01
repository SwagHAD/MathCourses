namespace Application.Interfaces
{
    public interface IPermissionCache
    {
        Task<bool> HasPermissionAsync(int userId, string permission, CancellationToken ct = default);
        Task<IReadOnlySet<string>> GetUserPermissionsAsync(int userId, CancellationToken ct = default);
    }
}
