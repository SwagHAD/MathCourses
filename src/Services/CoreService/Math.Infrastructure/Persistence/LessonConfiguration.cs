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
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired().HasMaxLength(256);
            builder.Property(l => l.Description).HasMaxLength(4000);
            // Удалить курс с уроками нельзя — сначала уроки
            builder.HasOne(l => l.CourseRef).WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId).OnDelete(DeleteBehavior.Restrict);
            // Уроки курса читаются по порядку — индекс сразу покрывает и фильтр, и сортировку
            builder.HasIndex(l => new { l.CourseId, l.Order });
        }
    }
}
