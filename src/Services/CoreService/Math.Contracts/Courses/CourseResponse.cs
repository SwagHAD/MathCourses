namespace CoreService.Contracts.Courses
{
    public sealed record CourseResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
