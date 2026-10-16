namespace CoreService.Contracts.Lessons
{
    public sealed record LessonResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
