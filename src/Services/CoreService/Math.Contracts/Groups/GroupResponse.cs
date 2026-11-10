namespace CoreService.Contracts.Groups
{
    public sealed record GroupResponse
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public long CourseId { get; init; }
        public string CourseName { get; init; } = null!;
    }
}
