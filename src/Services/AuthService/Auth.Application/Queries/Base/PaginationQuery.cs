using Domain.Base;

namespace Application.Queries.Base
{
    public class PaginationQuery<T> : IQuery<T[]> where T : BaseEntity
    {
        public int Page { get; init; }
        public int Count { get; init; }
        internal virtual IQueryable<T> ApplyFilter(IQueryable<T> query) => query;
    }
}
