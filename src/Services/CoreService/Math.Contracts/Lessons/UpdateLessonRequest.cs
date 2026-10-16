namespace CoreService.Contracts.Lessons
{
    public sealed record UpdateLessonRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int? GroupId { get; init; }
    }
}
