namespace Infrastructure.Events
{
    public sealed record StudentUserCreatedEvent
    {
        public int StudentId { get; init; }
        public int UserId { get; init; }
    }
}
