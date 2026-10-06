using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Студент", Secured = true)]
    public sealed class Student : BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public List<StudentGroup> StudentGroups { get; set; } = new();
    }
}
