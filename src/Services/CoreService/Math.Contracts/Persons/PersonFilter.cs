using CoreService.Contracts.Groups;
using SharedKernel.Contracts.Pagination;

namespace CoreService.Contracts.Persons
{
    public sealed record PersonFilter : IQueryFilter
    {
        /// <summary>Подстрока ФИО без учёта регистра.</summary>
        public string? Name { get; init; }
        public long? GroupId { get; init; }
        /// <summary>Только те, кто где-либо учится (Learner) или где-либо ведёт (Instructor).</summary>
        public MembershipKind? Kind { get; init; }
        /// <summary>true — только с учётной записью, false — только без неё.</summary>
        public bool? HasUser { get; init; }

        public IEnumerable<KeyValuePair<string, string?>> ToQuery()
        {
            yield return new("Filter.Name", Name);
            yield return new("Filter.GroupId", GroupId?.ToString());
            yield return new("Filter.Kind", Kind?.ToString());
            yield return new("Filter.HasUser", HasUser?.ToString());
        }
    }
}
