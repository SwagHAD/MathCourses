namespace CoreService.Contracts.Teachers
{
    /// <summary>Создаёт преподавателя и запрашивает у AuthService учётную запись для него.</summary>
    public sealed record CreateTeacherRequest
    {
        public string Name { get; init; } = null!;
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
