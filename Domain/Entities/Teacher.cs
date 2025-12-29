using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Учителя
    /// </summary>
    [Table("Teachers")]
    public class Teacher : BaseEntity<Teacher>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        public List<TeacherGroup> TeacherGroups { get; set; }

        protected override void CustomConfigure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
