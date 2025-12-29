using Domain.Entities;
using Domain.Entities.Base;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    internal sealed class MathDbContext : DbContext, IMathDbContext
    {
        private IDbContextTransaction? _currentTransaction;
        public MathDbContext(DbContextOptions<MathDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<TeacherGroup> TeacherGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
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

        async Task IMathDbContext.AddAsync(object entity, CancellationToken cancellationToken)
        {
            await base.AddAsync(entity, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MathDbContext).Assembly);
        }
    }
}
