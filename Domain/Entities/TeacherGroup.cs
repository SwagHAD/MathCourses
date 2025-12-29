using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities
{
    public sealed class TeacherGroup : BaseManyToManyEntity<Teacher, Group>
    {
    }
    public sealed class TeacherGroupConfiguration : BaseManyToManyConfiguration<TeacherGroup, Teacher, Group>
    {
        protected override void ConfigureMore(EntityTypeBuilder<TeacherGroup> builder)
        {
            builder.HasOne(x => x.FirstEntity)
                .WithMany(x => x.TeacherGroups)
                .HasForeignKey(x => x.FirstEntityId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder.HasOne(x => x.SecondEntity)
                .WithMany(x => x.TeacherGroups)
                .HasForeignKey(x => x.SecondEntityId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
