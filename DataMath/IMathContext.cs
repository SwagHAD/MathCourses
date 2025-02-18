using DataMath.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMath
{
    public interface IMathContext
    {
        DbSet<Teacher> Teachers { get; }
        DbSet<Group> Groups { get; }
        DbSet<Student> Students { get; }
        DbSet<StudentGroup> StudentGroups { get; }

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
