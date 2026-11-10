namespace CoreService.Contracts.Persons
{
    public sealed record PersonResponse
    {
        public long Id { get; init; }
        public string FullName { get; init; } = null!;
        public string? Email { get; init; }
        public bool HasUser { get; init; }
    }
}
