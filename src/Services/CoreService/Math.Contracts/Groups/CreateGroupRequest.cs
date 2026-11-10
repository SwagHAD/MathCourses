namespace CoreService.Contracts.Groups
{
    public sealed record CreateGroupRequest
    {
        public string Name { get; init; } = null!;
        public long CourseId { get; init; }
        public IReadOnlyList<GroupMemberRequest> Members { get; init; } = [];
    }
}
