using CoreService.Contracts.Groups;

namespace CoreService.Contracts.Students
{
    /// <summary>Студент вместе с группами, в которых он состоит.</summary>
    public sealed record StudentDetailsResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public IReadOnlyList<GroupResponse> Groups { get; init; } = [];
    }
}
