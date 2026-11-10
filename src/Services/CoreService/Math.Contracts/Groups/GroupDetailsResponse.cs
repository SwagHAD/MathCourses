namespace CoreService.Contracts.Groups
{
    /// <summary>Группа вместе с курсом и составом.</summary>
    public sealed record GroupDetailsResponse
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public long CourseId { get; init; }
        public string CourseName { get; init; } = null!;
        public IReadOnlyList<GroupMemberResponse> Members { get; init; } = [];
    }
}
