using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Группа — поток людей, проходящих один курс.
    /// </summary>
    [Title("Группа", Secured = true)]
    public sealed class Group : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long CourseId { get; set; }
        public Course CourseRef { get; set; } = null!;

        public List<GroupMembership> Members { get; set; } = new();
        public List<Session> Sessions { get; set; } = new();
    }
}
