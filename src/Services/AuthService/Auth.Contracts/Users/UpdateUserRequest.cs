namespace AuthService.Contracts.Users
{
    public sealed record UpdateUserRequest
    {
        public int Id { get; init; }
        public string Login { get; init; } = null!;
        /// <summary>Новый пароль в открытом виде; null — пароль не меняется. Хеширует сервер.</summary>
        public string? Password { get; init; }
        public int RoleId { get; init; }
    }
}
