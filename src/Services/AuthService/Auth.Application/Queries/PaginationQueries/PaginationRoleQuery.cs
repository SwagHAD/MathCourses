using Application.Queries.Base;
using Domain.Entities;

namespace Application.Queries.PaginationQueries
{
    public sealed class PaginationRoleQuery : PaginationQuery<Role>
    {
        public string NameSearch { get; set; } = null!;
        internal override IQueryable<Role> ApplyFilter(IQueryable<Role> query)
        {
            return query.Where(r => r.Name.Contains(NameSearch));
        }
    }
}
