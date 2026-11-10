using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class HomeworkSubmissionConfiguration : IEntityTypeConfiguration<HomeworkSubmission>
    {
        public void Configure(EntityTypeBuilder<HomeworkSubmission> builder)
        {
            builder.ToTable("HomeworkSubmissions");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.HomeworkRef)
                .WithMany()
                .HasForeignKey(x => x.HomeworkId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SessionRef)
                .WithMany()
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PersonRef)
                .WithMany()
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.FilePath)
                .IsRequired()
                .HasMaxLength(512);
            // «Мои сдачи» и «сдачи по занятию» — два основных запроса
            builder.HasIndex(x => new { x.PersonId, x.HomeworkId });
            builder.HasIndex(x => x.SessionId);
        }
    }
}
