namespace CoreService.Contracts.Groups
{
    /// <summary>Списки студентов и преподавателей заменяются целиком.</summary>
    public sealed record UpdateGroupRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int? CourseId { get; init; }
        public IReadOnlyList<int> StudentIds { get; init; } = [];
        public IReadOnlyList<int> TeacherIds { get; init; } = [];
    }
}
