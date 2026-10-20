namespace Infrastructure.Events
{
    public sealed record StudentUserCreatedEvent
    {
        public int PersonId { get; init; }
        public int UserId { get; init; }
    }
}