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
            if (filter.CourseId is int courseId)
                query = query.Where(g => g.CourseID == courseId);
            if (filter.StudentId is int studentId)
                query = query.Where(g => g.StudentGroups.Any(s => s.StudentID == studentId));
            if (filter.TeacherId is int teacherId)
                query = query.Where(g => g.TeacherGroups.Any(t => t.TeacherID == teacherId));
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Group, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Group, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = g => g.ID,
                ["name"] = g => g.Name,
                ["createdAt"] = g => g.CreatedAt,
            };

        protected override Expression<Func<Group, object>> DefaultSort => g => g.ID;

        protected override Expression<Func<Group, GroupResponse>> Projection =>
            g => new GroupResponse { Id = g.ID, Name = g.Name };
    }
}
