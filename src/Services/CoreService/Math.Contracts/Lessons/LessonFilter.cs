using SharedKernel.Contracts.Pagination;

namespace CoreService.Contracts.Lessons
{
    public sealed record LessonFilter : IQueryFilter
    {
        public long? CourseId { get; init; }
        /// <summary>Подстрока названия без учёта регистра.</summary>
        public string? Name { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.CourseId", CourseId?.ToString());
            yield return new("Filter.Name", Name);
        }
    }
}
