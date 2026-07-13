using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Name)
                .IsRequired().HasMaxLength(100);
            builder.HasIndex(t => t.UserId)
                .IsUnique();
        }
    }
}
