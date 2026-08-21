namespace Infrastructure.Events
{
    public sealed record TeacherUserCreatedEvent
    {
        public int TeacherId { get; init; }
        public int UserId { get; init; }
    }
}