using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Учителя
    /// </summary>
    [Title("Учитель")]
    public sealed class Teacher : BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public List<TeacherGroup> TeacherGroups { get; set; }
    }
}
