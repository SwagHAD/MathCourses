namespace CoreService.Contracts.Groups
{
    /// <summary>Участник группы в запросе на создание или изменение группы.</summary>
    public sealed record GroupMemberRequest
    {
        public long PersonId { get; init; }
        public MembershipKind Kind { get; init; }
        public MembershipStatus Status { get; init; } = MembershipStatus.Active;
    }
}
