namespace CoreService.Contracts.Lessons
{
    public sealed record CreateLessonRequest
    {
        public string Name { get; init; } = null!;
        public int? GroupId { get; init; }
    }
}
