using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Урок курса. Пишется один раз; когда и с какой группой он проходит — <see cref="Session"/>.
    /// </summary>
    [Title("Урок", Secured = true)]
    public sealed class Lesson : BaseEntity
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public Course CourseRef { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        /// <summary>Порядковый номер урока в курсе.</summary>
        public int Order { get; set; }

        public List<LessonMaterial> Materials { get; set; } = new();
        public List<Homework> Homeworks { get; set; } = new();
    }
}
