using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    public sealed class StreamConfiguration : IEntityTypeConfiguration<StreamLesson>
    {
        public void Configure(EntityTypeBuilder<StreamLesson> builder)
        {
            builder.ToTable("StreamLessons");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.LessonRef).WithMany()
                .HasForeignKey(f => f.LessonId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.RecordingPath)
                .HasMaxLength(512);
            builder.HasIndex(x => new { x.LessonId, x.Status });
        }
    }
}
