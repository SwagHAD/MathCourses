namespace CoreService.Contracts.Students
{
    /// <summary>Создаёт студента и запрашивает у AuthService учётную запись для него.</summary>
    public sealed record CreateStudentRequest
    {
        public string Name { get; init; } = null!;
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
