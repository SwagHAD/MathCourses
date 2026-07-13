using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");
            builder.HasKey(f => f.Id);
            builder.HasOne(f => f.SubmissionRef)
                .WithMany().HasForeignKey(f => f.SubmissionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(f => f.TeacherRef)
                .WithMany().HasForeignKey(f => f.TeacherId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(f => f.Score)
                .IsRequired();
        }
    }
}
