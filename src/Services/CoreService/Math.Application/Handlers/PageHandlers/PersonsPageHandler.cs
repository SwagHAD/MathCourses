using System.Linq.Expressions;
using CoreService.Contracts.Groups;
using CoreService.Contracts.Persons;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class PersonsPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Person, PersonFilter, PersonResponse>(dbContext)
    {
        protected override IQueryable<Person> ApplyFilter(IQueryable<Person> query, PersonFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(p => p.FullName.ToLower().Contains(name));
            }
            if (filter.GroupId is long groupId)
                query = query.Where(p => p.Memberships.Any(m => m.GroupId == groupId));
            if (filter.Kind is MembershipKind kind)
                query = query.Where(p => p.Memberships.Any(m => m.Kind == kind && m.Status == MembershipStatus.Active));
            if (filter.HasUser is bool hasUser)
                query = query.Where(p => (p.UserId != null) == hasUser);
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Person, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Person, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = p => p.Id,
                ["name"] = p => p.FullName,
                ["createdAt"] = p => p.CreatedAt,
            };

        protected override Expression<Func<Person, object>> DefaultSort => p => p.Id;

        protected override Expression<Func<Person, PersonResponse>> Projection =>
            p => new PersonResponse { Id = p.Id, FullName = p.FullName, Email = p.Email, HasUser = p.UserId != null };
    }
}
