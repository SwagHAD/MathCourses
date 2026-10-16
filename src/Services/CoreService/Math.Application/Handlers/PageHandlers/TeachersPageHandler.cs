using System.Linq.Expressions;
using CoreService.Contracts.Teachers;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class TeachersPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Teacher, TeacherFilter, TeacherResponse>(dbContext)
    {
        protected override IQueryable<Teacher> ApplyFilter(IQueryable<Teacher> query, TeacherFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(t => t.Name.ToLower().Contains(name));
            }
            if (filter.GroupId is int groupId)
                query = query.Where(t => t.TeacherGroups.Any(g => g.GroupID == groupId));
            if (filter.HasUser is bool hasUser)
                query = query.Where(t => (t.UserId != null) == hasUser);
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Teacher, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Teacher, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = t => t.ID,
                ["name"] = t => t.Name,
                ["createdAt"] = t => t.CreatedAt,
            };

        protected override Expression<Func<Teacher, object>> DefaultSort => t => t.ID;

        protected override Expression<Func<Teacher, TeacherResponse>> Projection =>
            t => new TeacherResponse { Id = t.ID, Name = t.Name };
    }
}
