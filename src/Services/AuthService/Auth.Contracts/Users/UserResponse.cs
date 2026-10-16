namespace AuthService.Contracts.Users
{
    public sealed record UserResponse
    {
        public int Id { get; init; }
        public string Login { get; init; } = null!;
        public int? RoleId { get; init; }
    }
}
