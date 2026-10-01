using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");
            builder.HasKey(l => l.ID);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(256);
            builder.HasOne(f => f.GroupRef).WithMany()
                .HasForeignKey(f => f.GroupID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
