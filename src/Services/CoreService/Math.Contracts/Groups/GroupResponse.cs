namespace CoreService.Contracts.Groups
{
    public sealed record GroupResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
