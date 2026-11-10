using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");
            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.GroupRef).WithMany(g => g.Sessions)
                .HasForeignKey(s => s.GroupId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(s => s.LessonRef).WithMany()
                .HasForeignKey(s => s.LessonId).OnDelete(DeleteBehavior.Restrict);
            // Расписание группы
            builder.HasIndex(s => new { s.GroupId, s.StartsAt });
            builder.HasIndex(s => s.LessonId);
        }
    }
}
