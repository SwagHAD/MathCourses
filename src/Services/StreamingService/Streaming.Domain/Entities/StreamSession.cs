using StreamingService.Domain.Enums;
using SharedKernel.Entities;

namespace StreamingService.Domain.Entities
{
    public sealed class StreamSession : BaseEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int UserId { get; set; }
        public string StreamPath { get; set; } = null!;
        public string StreamToken { get; set; } = null!;
        public StreamStatus StreamStatus { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public string? RecordingPath { get; set; }
    }
}
