using SharedKernel.Contracts.Pagination;

namespace CoreService.Contracts.Groups
{
    public sealed record GroupFilter : IQueryFilter
    {
        /// <summary>Подстрока названия без учёта регистра.</summary>
        public string? Name { get; init; }
        public long? CourseId { get; init; }
        /// <summary>Группы, в которых состоит человек.</summary>
        public long? PersonId { get; init; }
        /// <summary>Вместе с <see cref="PersonId"/>: только где он учится или только где ведёт.</summary>
        public MembershipKind? Kind { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
            yield return new("Filter.CourseId", CourseId?.ToString());
            yield return new("Filter.PersonId", PersonId?.ToString());
            yield return new("Filter.Kind", Kind?.ToString());
        }
    }
}
