namespace CoreService.Contracts.Students
{
    public sealed record UpdateStudentRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
