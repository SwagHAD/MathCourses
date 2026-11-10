namespace CoreService.Contracts.Persons
{
    /// <summary>
    /// Создаёт человека. Если передан <see cref="Account"/> — AuthService заведёт ему учётную запись.
    /// </summary>
    public sealed record CreatePersonRequest
    {
        public string Name { get; init; } = null!;
        public string? Surname { get; init; }
        public string? Patronymic { get; init; }
        public short Age { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public PersonAccountRequest? Account { get; init; }
    }

    public sealed record PersonAccountRequest
    {
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
