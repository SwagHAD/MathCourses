namespace CoreService.Contracts.Students
{
    public sealed record StudentResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
