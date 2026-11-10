namespace CoreService.Contracts.Courses
{
    public sealed record CreateCourseRequest
    {
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
    }
}
