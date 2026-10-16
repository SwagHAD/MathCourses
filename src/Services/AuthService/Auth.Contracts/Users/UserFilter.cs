using SharedKernel.Contracts.Pagination;

namespace AuthService.Contracts.Users
{
    public sealed record UserFilter : IQueryFilter
    {
        /// <summary>Подстрока логина без учёта регистра.</summary>
        public string? Login { get; init; }
        public int? RoleId { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Login", Login);
            yield return new("Filter.RoleId", RoleId?.ToString());
        }
    }
}
