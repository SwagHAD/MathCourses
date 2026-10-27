using SharedKernel.Cache;
using SharedKernel.Interfaces;
using StackExchange.Redis;

namespace SharedKernel.AspNetCore.Redis
{
    internal sealed class RedisPermissionCache(IConnectionMultiplexer redis, IUserProvider userProvider) : IPermissionCacheService
    {
        private readonly IDatabase _db = redis.GetDatabase();
        // Доступы записывает AuthService при логине, остальные сервисы читают по тому же ключу
        private static readonly string KeyPrefix = "auth:permissions:";
        private static readonly TimeSpan Ttl = TimeSpan.FromHours(8);

        public async Task<IReadOnlySet<string>> GetCurrentUserPermissionsAsync(CancellationToken ct = default)
        {
            var permissions = await _db.SetMembersAsync(KeyPrefix + userProvider.GetUserId());
            return permissions.Select(p => p.ToString()).ToHashSet();
        }

        public async Task RemoveUserPermissionsAsync(int userId, CancellationToken ct = default)
        {
            await _db.KeyDeleteAsync(KeyPrefix + userId);
        }

        public async Task SetUserPermissionsAsync(int userId, IEnumerable<string> permissions, CancellationToken ct = default)
        {
            var values = permissions.Select(p => (RedisValue)p).ToArray();
            if (!values.Any())
                return;

            var key = KeyPrefix + userId;
            var tran = _db.CreateTransaction();
            _ = tran.KeyDeleteAsync(key);
            _ = tran.SetAddAsync(key, values);
            _ = tran.KeyExpireAsync(key, Ttl);
            await tran.ExecuteAsync();
        }

        public async Task<bool> HasPermissionAsync(string permission, CancellationToken ct = default)
        {
            return await _db.SetContainsAsync(KeyPrefix + userProvider.GetUserId(), permission);
        }
    }
}
