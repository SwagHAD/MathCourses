using StackExchange.Redis;
using StreamingService.Application.Common.Interfaces;

namespace StreamingService.Infrastructure.Redis
{
    internal sealed class RedisPermissionCache(IConnectionMultiplexer redis) : IPermissionCache
    {
        private IDatabase _db = redis.GetDatabase();
        private static readonly string KeyPrefix = "streaming:permissions:";
        public async Task<IReadOnlySet<string>> GetUserPermissionsAsync(int userId, CancellationToken ct = default)
        {
            var permissions = await _db.SetMembersAsync(KeyPrefix + userId);
            return permissions.Select(p => p.ToString()).ToHashSet();
        }

        public async Task<bool> HasPermissionAsync(int userId, string permission, CancellationToken ct = default)
        {
            return await _db.SetContainsAsync(KeyPrefix + userId, permission);
        }
    }
}
