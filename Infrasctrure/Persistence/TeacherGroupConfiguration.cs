using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    internal sealed class TeacherGroupConfiguration : IEntityTypeConfiguration<TeacherGroup>
    {
        public void Configure(EntityTypeBuilder<TeacherGroup> builder)
        {
            builder.ToTable("TeacherGroups");
            builder.HasKey(tg => new { tg.TeacherID, tg.GroupID });
            builder.HasOne(tg => tg.TeacherRef)
                .WithMany(t => t.TeacherGroups)
                .HasForeignKey(tg => tg.TeacherID);
            builder.HasOne(tg => tg.GroupRef)
                .WithMany(g => g.TeacherGroups)
                .HasForeignKey(tg => tg.GroupID);
        }
    }
}
