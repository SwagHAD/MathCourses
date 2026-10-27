using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Студент", Secured = true)]
    public sealed class Student : BaseEntity
    {
        public int StudentId { get; set; }
        public Person PersonRef { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
    }
}
