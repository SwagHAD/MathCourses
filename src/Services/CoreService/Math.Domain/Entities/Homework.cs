using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Домашнее задание", Secured = true)]
    public sealed class Homework : BaseEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson LessonRef { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset Deadline { get; set; }
    }
}
