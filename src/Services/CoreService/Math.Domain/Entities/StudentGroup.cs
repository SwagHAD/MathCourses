using SharedKernel.Attributes;
using Domain.Enums;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("СтудентГруппа")]
    public sealed class StudentGroup : BaseEntity
    {
        public int StudentID { get; set; }
        public Student StudentRef { get; set; }
        public int GroupID { get; set; }
        public Group GroupRef { get; set; }
        public StudentStatus? StudentStatus { get; set; }
    }
}
