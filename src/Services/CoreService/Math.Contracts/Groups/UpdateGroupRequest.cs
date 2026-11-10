namespace CoreService.Contracts.Groups
{
    /// <summary>Состав группы заменяется целиком: кого нет в списке — удаляется из группы.</summary>
    public sealed record UpdateGroupRequest
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public long CourseId { get; init; }
        public IReadOnlyList<GroupMemberRequest> Members { get; init; } = [];
    }
}
