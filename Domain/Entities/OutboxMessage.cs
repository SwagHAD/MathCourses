using Domain.Entities.Base;

namespace Domain.Entities
{
    public sealed class OutboxMessage : BaseEntity
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string Payload { get; set; } = null!;
        public DateTimeOffset? ProcessedAt { get; set; }
        public string? Error { get; set; }
        public int RetryCount { get; set; }
    }
}
