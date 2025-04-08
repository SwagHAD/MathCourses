using DataMath.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataMath
{
    public class MathContext : DbContext, IMathContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }

        public MathContext(DbContextOptions<MathContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentGroup>()
                .HasKey(sg => sg.Id);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Student)
                .WithMany()
                .HasForeignKey(sg => sg.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Group)
                .WithMany()
                .HasForeignKey(sg => sg.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentGroup>()
                .HasIndex(sg => new { sg.StudentId, sg.GroupId })
                .IsUnique();
        }
    }
}
