using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.ID);
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(s => s.UserId)
                .IsUnique();
        }
    }
}
