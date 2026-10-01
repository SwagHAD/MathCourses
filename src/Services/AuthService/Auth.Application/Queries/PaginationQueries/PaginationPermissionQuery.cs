using Application.Queries.Base;
using Domain.Entities;

namespace Application.Queries.PaginationQueries
{
    public sealed class PaginationPermissionQuery : PaginationQuery<Permission>
    {
        public string NameSearch { get; set; } = null!;
        internal override IQueryable<Permission> ApplyFilter(IQueryable<Permission> query)
        {
            return query.Where(p => p.Name.Contains(NameSearch));
        }
    }
}
