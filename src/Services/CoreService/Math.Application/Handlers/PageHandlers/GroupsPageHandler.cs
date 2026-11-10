using System.Linq.Expressions;
using CoreService.Contracts.Groups;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class GroupsPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Group, GroupFilter, GroupResponse>(dbContext)
    {
        protected override IQueryable<Group> ApplyFilter(IQueryable<Group> query, GroupFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(g => g.Name.ToLower().Contains(name));
            }
            if (filter.CourseId is long courseId)
                query = query.Where(g => g.CourseId == courseId);
            if (filter.PersonId is long personId)
            {
                var kind = filter.Kind;
                query = query.Where(g => g.Members.Any(m => m.PersonId == personId && (kind == null || m.Kind == kind)));
            }
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Group, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Group, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = g => g.Id,
                ["name"] = g => g.Name,
                ["course"] = g => g.CourseRef.Name,
                ["createdAt"] = g => g.CreatedAt,
            };

        protected override Expression<Func<Group, object>> DefaultSort => g => g.Id;

        protected override Expression<Func<Group, GroupResponse>> Projection =>
            g => new GroupResponse { Id = g.Id, Name = g.Name, CourseId = g.CourseId, CourseName = g.CourseRef.Name };
    }
}
