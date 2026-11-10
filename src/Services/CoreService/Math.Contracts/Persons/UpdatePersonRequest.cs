namespace CoreService.Contracts.Persons
{
    public sealed record UpdatePersonRequest
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Surname { get; init; }
        public string? Patronymic { get; init; }
        public short Age { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
    }
}
