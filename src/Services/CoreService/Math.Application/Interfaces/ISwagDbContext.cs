using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Interfaces
{
    public interface ISwagDbContext : IAsyncDisposable
    {
        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }
        DbSet<TeacherGroup> TeacherGroups { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<OutboxMessage> OutboxMessages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken cancellation = default);
        Task CommitTransactionAsync(CancellationToken cancellation = default);
        Task RollbackTransactionAsync(CancellationToken cancellation = default);
        Task MigrateAsync(CancellationToken cancellationToken = default);
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
