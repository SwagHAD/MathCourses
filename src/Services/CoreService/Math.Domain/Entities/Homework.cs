using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Задание урока — общее для всех групп. Срок сдачи у каждой группы свой: <see cref="Session.HomeworkDueAt"/>.
    /// </summary>
    [Title("Домашнее задание", Secured = true)]
    public sealed class Homework : BaseEntity
    {
        public long Id { get; set; }
        public long LessonId { get; set; }
        public Lesson LessonRef { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
