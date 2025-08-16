using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Группы
    /// </summary>
    [Table("Groups")]
    public class Group : BaseEntity<Group>
    {
        /// <summary>
        /// Имя группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Учитель
        /// </summary>
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        /// <summary>
        /// Направление группы
        /// </summary>
        public int? CourseID { get; set; }
        public Course Course { get; set; }
        public ICollection<StudentGroup> StudentGroups { get; set; }

        protected override void CustomConfigure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(50);
            builder.HasOne(x => x.Teacher).WithMany(x => x.Groups).HasForeignKey(x => x.TeacherID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
