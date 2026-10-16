using SharedKernel.Contracts.Pagination;

namespace CoreService.Contracts.Courses
{
    public sealed record CourseFilter : IQueryFilter
    {
        /// <summary>Подстрока названия без учёта регистра.</summary>
        public string? Name { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
        }
    }
}
