using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Направление
    /// </summary>
    [Table("Courses")]
    public class Course : BaseEntity<Course>
    {
        /// <summary>
        /// Название направления
        /// </summary>
        public string Name { get; set; }

        protected override void CustomConfigure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
