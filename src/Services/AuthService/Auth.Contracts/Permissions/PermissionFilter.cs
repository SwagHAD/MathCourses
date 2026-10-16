using SharedKernel.Contracts.Pagination;

namespace AuthService.Contracts.Permissions
{
    public sealed record PermissionFilter : IQueryFilter
    {
        /// <summary>Подстрока названия без учёта регистра.</summary>
        public string? Name { get; init; }
        /// <summary>Имя типа объекта, например Student.</summary>
        public string? ObjectType { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
            yield return new("Filter.ObjectType", ObjectType);
        }
    }
}
