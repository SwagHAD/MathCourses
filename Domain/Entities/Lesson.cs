using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Уроки
    /// </summary>
    [Table("Lessons")]
    public class Lesson : BaseEntity<Lesson>
    {
        /// <summary>
        /// Название урока
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public int? GroupID { get; set; }
        public Group Group { get; set; }

        protected override void CustomConfigure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .HasMaxLength(150);
            builder.HasOne(x  => x.Group).WithMany().HasForeignKey(x => x.GroupID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
