using SharedKernel.Attributes;
using SharedKernel.Entities;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Трансляция занятия. Висит на занятии, а не на уроке: у каждой группы свой эфир.
    /// </summary>
    [Title("Стрим", Secured = true)]
    public sealed class SessionStream : BaseEntity
    {
        public long Id { get; set; }
        public long SessionId { get; set; }
        public Session SessionRef { get; set; } = null!;
        public string Title { get; set; } = null!;
        public StreamStatus Status { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public DateTimeOffset? EndedAt { get; set; }
        public string? RecordingPath { get; set; }
    }
}
