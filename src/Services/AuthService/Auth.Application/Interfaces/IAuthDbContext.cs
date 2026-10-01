using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAuthDbContext : IAsyncDisposable
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RefreshTokenSession> RefreshTokenSessions { get; set; }
        public DbSet<ObjectType> ObjectTypes { get; set;  }
        public Task MigrateAsync(CancellationToken cancellationToken = default);
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        public Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public DbSet<T> Set<T>() where T : class;
    }
}
