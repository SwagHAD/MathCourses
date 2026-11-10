using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class SessionStreamConfiguration : IEntityTypeConfiguration<SessionStream>
    {
        public void Configure(EntityTypeBuilder<SessionStream> builder)
        {
            builder.ToTable("SessionStreams");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.SessionRef).WithMany(s => s.Streams)
                .HasForeignKey(x => x.SessionId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.RecordingPath)
                .HasMaxLength(512);
            builder.HasIndex(x => new { x.SessionId, x.Status });
        }
    }
}
