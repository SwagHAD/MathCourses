using Domain.Attributes;
using Domain.Enums;

namespace Domain.Entities
{
    [Title("УчительГруппа")]
    public sealed class StudentGroup
    {
        public int StudentID { get; set; }
        public Student StudentRef { get; set; }
        public int GroupID { get; set; }
        public Group GroupRef { get; set; }
        public StudentStatus? StudentStatus { get; set; }
    }
}
