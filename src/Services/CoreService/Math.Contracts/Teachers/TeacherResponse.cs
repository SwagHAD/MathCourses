namespace CoreService.Contracts.Teachers
{
    public sealed record TeacherResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
