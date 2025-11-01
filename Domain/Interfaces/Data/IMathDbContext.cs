using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces.Data
{
    public interface IMathDbContext : IAsyncDisposable
    {
        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken cancellation = default);
        Task CommitTransactionAsync(CancellationToken cancellation = default);
        Task RollbackTransactionAsync(CancellationToken cancellation = default);
        Task MigrateAsync(CancellationToken cancellationToken = default);
        Task AddAsync(object entity, CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    }
}
