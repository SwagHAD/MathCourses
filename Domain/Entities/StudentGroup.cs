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
    public class StudentGroup : BaseEntity4M2M<Student, Group>
    {
        public StudentStatus StudentStatus { get; set; }
        protected override void CustomCofigure(EntityTypeBuilder<BaseEntity4M2M<Student, Group>> builder)
        {
            throw new NotImplementedException();
        }
    }
}
