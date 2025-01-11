using DataMath.Entities;
using DataMath.TableParts;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связь "один-ко-многим" между Group и Teacher
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь "многие-ко-многим" через таблицу StudentGroup
            modelBuilder.Entity<StudentGroup>()
                .HasKey(sg => sg.Id);

            modelBuilder.Entity<StudentGroup>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(sg => sg.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentGroup>()
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(sg => sg.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
