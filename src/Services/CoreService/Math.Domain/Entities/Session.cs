using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Занятие: конкретная группа проходит конкретный урок в конкретное время.
    /// </summary>
    [Title("Занятие", Secured = true)]
    public sealed class Session : BaseEntity
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public Group GroupRef { get; set; } = null!;
        public long LessonId { get; set; }
        public Lesson LessonRef { get; set; } = null!;
        public DateTimeOffset StartsAt { get; set; }
        public DateTimeOffset? EndsAt { get; set; }
        /// <summary>Срок сдачи домашних заданий урока для этой группы.</summary>
        public DateTimeOffset? HomeworkDueAt { get; set; }

        public List<SessionStream> Streams { get; set; } = new();
    }
}
