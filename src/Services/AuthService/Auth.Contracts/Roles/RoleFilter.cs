using SharedKernel.Contracts.Pagination;

namespace AuthService.Contracts.Roles
{
    public sealed record RoleFilter : IQueryFilter
    {
        /// <summary>Подстрока названия без учёта регистра.</summary>
        public string? Name { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
        }
    }
}
