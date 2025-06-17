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

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public MathContext(DbContextOptions<MathContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>(f =>
            {
                f.HasKey(t => t.Id);
                f.Property(t => t.Name)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Group>(f =>
            {
                f.HasKey(g => g.Id);
                f.Property(g => g.Name)
                    .HasMaxLength(10);

                f.HasOne(g => g.Teacher)
                    .WithMany(t => t.Groups)
                    .HasForeignKey(g => g.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Student>(f =>
            {
                f.HasKey(s => s.Id);

                f.Property(s => s.Name)
                    .HasMaxLength(50);

                f.HasMany(s => s.Groups)
                 .WithMany(g => g.Students)
                 .UsingEntity<StudentGroup>(
                    joinEntity => joinEntity
                        .HasOne(sg => sg.Group)
                        .WithMany(g => g.StudentGroups)
                        .HasForeignKey(sg => sg.GroupId),

                    joinEntity => joinEntity
                        .HasOne(sg => sg.Student)
                        .WithMany(s => s.StudentGroups)
                        .HasForeignKey(sg => sg.StudentId),

                    joinEntity =>
                    {
                        joinEntity.HasKey(sg => new { sg.StudentId, sg.GroupId });
                        joinEntity.Property(sg => sg.JoinedAt)
                                  .HasColumnType("timestamp");
                        joinEntity.ToTable("Student_Group");
                    });
            });
        }
    }
}
