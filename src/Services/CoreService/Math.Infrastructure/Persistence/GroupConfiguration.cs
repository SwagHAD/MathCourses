using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(g => g.CourseRef).WithMany(c => c.Groups)
                .HasForeignKey(g => g.CourseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(g => g.CourseId);
        }
    }
}
