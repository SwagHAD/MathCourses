using SharedKernel.Cache;
using SharedKernel.Interfaces;
using StackExchange.Redis;
using System.Security;

namespace SharedKernel.AspNetCore.Redis
{
    internal sealed class RedisRoleCache(IConnectionMultiplexer redis, IUserProvider userProvider) : IRoleCacheService
    {
        private readonly IDatabase _db = redis.GetDatabase();
        private static readonly string KeyPrefix = "auth:roles:";
        private static readonly TimeSpan Ttl = TimeSpan.FromHours(8);
        public async Task<IReadOnlySet<int>> GetCurrentUserRolesIdAsync(CancellationToken ct = default)
        {
            var roles = await _db.SetMembersAsync(KeyPrefix + userProvider.GetUserId());
            return roles.Select(p => (int)p).ToHashSet();
        }

        public async Task RemoveUserRolesAsync(int userId, CancellationToken ct = default)
        {
            await _db.KeyDeleteAsync(KeyPrefix + userId);
        }

        public async Task SetUserRolesAsync(int userId, IEnumerable<int> roles, CancellationToken ct = default)
        {
            var values = roles.Select(p => (RedisValue)p).ToArray();
            if (!values.Any())
                return;

            var key = KeyPrefix + userId;
            var tran = _db.CreateTransaction();
            _ = tran.KeyDeleteAsync(key);
            _ = tran.SetAddAsync(key, values);
            _ = tran.KeyExpireAsync(key, Ttl);
            await tran.ExecuteAsync();
        }

        public async Task<bool> HasRoleAsync(int roleId, CancellationToken ct = default)
        {
            return await _db.SetContainsAsync(KeyPrefix + userProvider.GetUserId(), roleId);
        }
    }
}
