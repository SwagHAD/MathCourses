namespace Infrastructure.Events
{
    /// <summary>CoreService → AuthService: заведи учётную запись для человека.</summary>
    public sealed record PersonAccountRequestedEvent
    {
        public long PersonId { get; init; }
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
