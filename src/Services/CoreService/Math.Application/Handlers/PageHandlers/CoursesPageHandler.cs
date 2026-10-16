using System.Linq.Expressions;
using CoreService.Contracts.Courses;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class CoursesPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Course, CourseFilter, CourseResponse>(dbContext)
    {
        protected override IQueryable<Course> ApplyFilter(IQueryable<Course> query, CourseFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(name));
            }
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Course, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Course, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = c => c.ID,
                ["name"] = c => c.Name,
                ["createdAt"] = c => c.CreatedAt,
            };

        protected override Expression<Func<Course, object>> DefaultSort => c => c.ID;

        protected override Expression<Func<Course, CourseResponse>> Projection =>
            c => new CourseResponse { Id = c.ID, Name = c.Name };
    }
}
