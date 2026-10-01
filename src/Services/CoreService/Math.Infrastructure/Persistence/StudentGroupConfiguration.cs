using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class StudentGroupConfiguration : IEntityTypeConfiguration<StudentGroup>
    {
        public void Configure(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.ToTable("StudentGroups");
            builder.HasKey(sg => new { sg.StudentID, sg.GroupID });
            builder.HasOne(sg => sg.StudentRef)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(sg => sg.StudentID);
            builder.HasOne(sg => sg.GroupRef)
                .WithMany(g => g.StudentGroups)
                .HasForeignKey(sg => sg.GroupID);
        }
    }
}
