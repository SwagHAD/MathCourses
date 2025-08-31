using Domain.Entities;
using Domain.Entities.Base;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data
{
    public class MathDbContext : DbContext, IMathDbContext
    {
        private IDbContextTransaction? _currentTransaction;
        public MathDbContext(DbContextOptions<MathDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public async Task BeginTransaction()
        {
            if(_currentTransaction == null)
            {
                _currentTransaction = await Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            try
            {
                await SaveChangesAsync();
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
            catch
            {
                await RollbackTransaction();
                throw;
            }
        }

        public async Task RollbackTransaction()
        {
            if(_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MathDbContext).Assembly);
        }
    }
}
