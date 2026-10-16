using SharedKernel.Contracts.Pagination;

namespace CoreService.Contracts.Teachers
{
    public sealed record TeacherFilter : IQueryFilter
    {
        /// <summary>Подстрока имени без учёта регистра.</summary>
        public string? Name { get; init; }
        public int? GroupId { get; init; }
        /// <summary>true — только с учётной записью, false — только без неё.</summary>
        public bool? HasUser { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
            yield return new("Filter.GroupId", GroupId?.ToString());
            yield return new("Filter.HasUser", HasUser?.ToString());
        }
    }
}
