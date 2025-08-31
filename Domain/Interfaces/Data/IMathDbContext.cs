using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces.Data
{
    public interface IMathDbContext : IDisposable
    {
        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Lesson> Lessons { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
