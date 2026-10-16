using CoreService.Contracts.Students;
using CoreService.Contracts.Teachers;

namespace CoreService.Contracts.Groups
{
    /// <summary>Группа вместе со студентами и преподавателями.</summary>
    public sealed record GroupDetailsResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public IReadOnlyList<StudentResponse> Students { get; init; } = [];
        public IReadOnlyList<TeacherResponse> Teachers { get; init; } = [];
    }
}
