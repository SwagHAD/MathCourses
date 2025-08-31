using Domain.Entities.Base;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Промежуточная таблица для связи Student-Group (многие-ко-многим)
    /// </summary>
    [Table("StudentGroups")]
    public class StudentGroup : BaseManyToManyEntity<Student, Group>
    {
        public StudentStatus? StudentStatus { get; set; }
    }
    public class StudentGroupConfiguration : BaseManyToManyConfiguration<StudentGroup, Student, Group>
    {
        protected override void ConfigureMore(EntityTypeBuilder<StudentGroup> builder)
        {
            builder.Property(x => x.StudentStatus)
                .IsRequired()
                .HasDefaultValue(StudentStatus.Studying);
            builder.HasOne(x => x.FirstEntity)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.FirstEntityId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.SecondEntity)
                .WithMany(x => x.StudentGroups)
                .HasForeignKey(x => x.SecondEntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
