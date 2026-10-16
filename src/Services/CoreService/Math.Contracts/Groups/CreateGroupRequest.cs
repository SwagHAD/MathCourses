namespace CoreService.Contracts.Groups
{
    public sealed record CreateGroupRequest
    {
        public string Name { get; init; } = null!;
        public int? CourseId { get; init; }
        public IReadOnlyList<int> StudentIds { get; init; } = [];
        public IReadOnlyList<int> TeacherIds { get; init; } = [];
    }
}
