using CoreService.Contracts.Groups;

namespace CoreService.Contracts.Persons
{
    /// <summary>Человек вместе с группами, где он учится или которые ведёт.</summary>
    public sealed record PersonDetailsResponse
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Surname { get; init; }
        public string? Patronymic { get; init; }
        public string FullName { get; init; } = null!;
        public short Age { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public int? UserId { get; init; }
        public IReadOnlyList<PersonGroupResponse> Groups { get; init; } = [];
    }

    public sealed record PersonGroupResponse
    {
        public long GroupId { get; init; }
        public string GroupName { get; init; } = null!;
        public MembershipKind Kind { get; init; }
        public MembershipStatus Status { get; init; }
    }
}
