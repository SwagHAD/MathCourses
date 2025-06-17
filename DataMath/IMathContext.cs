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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
