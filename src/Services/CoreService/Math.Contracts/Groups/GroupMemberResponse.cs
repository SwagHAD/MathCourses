namespace CoreService.Contracts.Groups
{
    public sealed record GroupMemberResponse
    {
        public long PersonId { get; init; }
        public string FullName { get; init; } = null!;
        public MembershipKind Kind { get; init; }
        public MembershipStatus Status { get; init; }
        public DateTimeOffset JoinedAt { get; init; }
    }
}
