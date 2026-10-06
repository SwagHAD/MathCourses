using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamingService.Domain.Entities;

namespace StreamingService.Infrastructure.Persistence
{
    internal sealed class StreamSessionConfiguration : IEntityTypeConfiguration<StreamSession>
    {
        public void Configure(EntityTypeBuilder<StreamSession> builder)
        {
            builder.ToTable("StreamSessions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LessonId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.StreamPath).IsRequired().HasMaxLength(256);
            builder.Property(x => x.StreamToken).IsRequired().HasMaxLength(256);
            builder.Property(x => x.StartedAt).IsRequired();
            builder.Property(x => x.RecordingPath).HasMaxLength(256);
            builder.HasIndex(x => new { x.LessonId, x.UserId });
            builder.HasIndex(x => x.StreamToken).IsUnique();
        }
    }
}
