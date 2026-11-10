namespace CoreService.Contracts.Lessons
{
    /// <summary>Курс у урока не меняется: перенос урока в другой курс — это новый урок.</summary>
    public sealed record UpdateLessonRequest
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public int Order { get; init; }
    }
}
