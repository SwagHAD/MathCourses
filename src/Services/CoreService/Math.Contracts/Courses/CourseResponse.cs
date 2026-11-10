namespace CoreService.Contracts.Courses
{
    public sealed record CourseResponse
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
