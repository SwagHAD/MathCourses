using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Направление (курс) — содержание обучения: набор уроков, общий для всех групп.
    /// </summary>
    [Title("Направление", Secured = true)]
    public sealed class Course : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public List<Lesson> Lessons { get; set; } = new();
        public List<Group> Groups { get; set; } = new();
    }
}
