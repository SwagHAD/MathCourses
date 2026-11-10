namespace Infrastructure.Events
{
    /// <summary>AuthService → CoreService: учётная запись создана, привяжи её к человеку.</summary>
    public sealed record PersonUserCreatedEvent
    {
        public long PersonId { get; init; }
        public int UserId { get; init; }
    }
}
