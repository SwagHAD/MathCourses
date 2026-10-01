using Application.Queries.Base;
using Domain.Entities;

namespace Application.Queries.PaginationQueries
{
    public sealed class PaginationUserQuery : PaginationQuery<User>
    {
        public string LoginSearch { get; set; } = null!;
        internal override IQueryable<User> ApplyFilter(IQueryable<User> query)
        {
            return query.Where(u => u.Login.Contains(LoginSearch));
        }
    }
}
