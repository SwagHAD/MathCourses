using DataMath.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMath
{
    public interface IMathContext
    {
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<StudentGroup> StudentGroups { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
