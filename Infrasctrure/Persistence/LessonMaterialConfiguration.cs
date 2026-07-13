using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class LessonMaterialConfiguration : IEntityTypeConfiguration<LessonMaterial>
    {
        public void Configure(EntityTypeBuilder<LessonMaterial> builder)
        {
            builder.ToTable("LessonMaterials");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.LessonRef)
                .WithMany()
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.FilePath)
                .IsRequired()
                .HasMaxLength(512);
            builder.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}
