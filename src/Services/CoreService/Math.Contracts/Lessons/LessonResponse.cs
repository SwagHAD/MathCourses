namespace CoreService.Contracts.Lessons
{
    public sealed record LessonResponse
    {
        public long Id { get; init; }
        public long CourseId { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public int Order { get; init; }
    }
}
