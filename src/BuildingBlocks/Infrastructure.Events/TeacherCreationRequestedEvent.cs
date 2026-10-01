namespace Infrastructure.Events
{
    public sealed record TeacherCreationRequestedEvent
    {
        public int TeacherId { get; init; }
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}