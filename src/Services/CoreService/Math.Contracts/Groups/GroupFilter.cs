using SharedKernel.Contracts.Pagination;

namespace CoreService.Contracts.Groups
{
    public sealed record GroupFilter : IQueryFilter
    {
        /// <summary>Подстрока названия без учёта регистра.</summary>
        public string? Name { get; init; }
        public int? CourseId { get; init; }
        /// <summary>Группы, в которых состоит студент.</summary>
        public int? StudentId { get; init; }
        /// <summary>Группы, которые ведёт преподаватель.</summary>
        public int? TeacherId { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
            yield return new("Filter.CourseId", CourseId?.ToString());
            yield return new("Filter.StudentId", StudentId?.ToString());
            yield return new("Filter.TeacherId", TeacherId?.ToString());
        }
    }
}
