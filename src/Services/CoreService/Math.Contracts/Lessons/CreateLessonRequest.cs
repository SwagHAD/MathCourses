namespace CoreService.Contracts.Lessons
{
    public sealed record CreateLessonRequest
    {
        public long CourseId { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public int Order { get; init; }
    }
}
