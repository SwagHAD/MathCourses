namespace AuthService.Contracts.Users
{
    public sealed record CreateUserRequest
    {
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
        public int RoleId { get; init; }
    }
}
