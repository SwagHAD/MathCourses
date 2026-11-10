namespace CoreService.Contracts.Courses
{
    public sealed record UpdateCourseRequest
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
