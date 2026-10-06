using SharedKernel.Attributes;
using SharedKernel.Entities;
using Domain.Enums;

namespace Domain.Entities
{
    [Title("Стрим", Secured = true)]
    public sealed class StreamLesson : BaseEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public Lesson LessonRef { get; set; } = null!;
        public string Title { get; set; } = null!;
        public StreamStatus Status { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? EndedAt { get; set; }
        public string? RecordingPath { get; set; }
    }
}
