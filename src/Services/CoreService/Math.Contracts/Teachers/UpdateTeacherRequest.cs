namespace CoreService.Contracts.Teachers
{
    public sealed record UpdateTeacherRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
