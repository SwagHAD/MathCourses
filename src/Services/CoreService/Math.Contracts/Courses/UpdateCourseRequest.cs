namespace CoreService.Contracts.Courses
{
    public sealed record UpdateCourseRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
