using Domain.Entities;
using Domain.Entities.Base;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Postgres
{
    public sealed class SwagDbContext : DbContext, ISwagDbContext
    {
        private IDbContextTransaction? _currentTransaction;
        public SwagDbContext(DbContextOptions<SwagDbContext> options) : base(options){ }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<TeacherGroup> TeacherGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public override DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public async Task BeginTransactionAsync(CancellationToken cancellation = default)
        {
            if(_currentTransaction == null || Database.CurrentTransaction == null)
            {
                _currentTransaction = Database.CurrentTransaction ?? await Database.BeginTransactionAsync(cancellation);
            }
        }

        public async Task CommitTransactionAsync(CancellationToken cancellation = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(cancellation);
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
                else
                {
                    throw new Exception("Transaction was not started");
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellation = default)
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellation);
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
            else
            {
                throw new Exception("Transaction was not started");
            }
        }
        public async Task MigrateAsync(CancellationToken cancellationToken)
        {
            await Database.MigrateAsync(cancellationToken);
        }

        public async override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken)
        {
            return await base.AddAsync(entity, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwagDbContext).Assembly);
        }
        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
