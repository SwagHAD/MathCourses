namespace Infrastructure.Events
{
    public sealed record TeacherUserCreatedEvent
    {
        public int PersonId { get; init; }
        public int UserId { get; init; }
    }
}