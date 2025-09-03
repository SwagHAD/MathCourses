using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Pagination
{
    [Keyless]
    public class PagedResult<TEntity> where TEntity : BaseEntity
    {
        public IReadOnlyList<TEntity> Items { get; init; } = [];
        public int TotalCount { get; init; }

        public int Page {  get; init; }

        public int PageSize { get; init; }
    }
}
