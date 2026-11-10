using System.Linq.Expressions;
using CoreService.Contracts.Lessons;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class LessonsPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Lesson, LessonFilter, LessonResponse>(dbContext)
    {
        protected override IQueryable<Lesson> ApplyFilter(IQueryable<Lesson> query, LessonFilter filter)
        {
            if (filter.CourseId is long courseId)
                query = query.Where(l => l.CourseId == courseId);
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(l => l.Name.ToLower().Contains(name));
            }
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Lesson, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Lesson, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = l => l.Id,
                ["name"] = l => l.Name,
                ["order"] = l => l.Order,
                ["createdAt"] = l => l.CreatedAt,
            };

        // Внутри курса естественный порядок — по номеру урока
        protected override Expression<Func<Lesson, object>> DefaultSort => l => l.Order;

        protected override Expression<Func<Lesson, LessonResponse>> Projection =>
            l => new LessonResponse { Id = l.Id, CourseId = l.CourseId, Name = l.Name, Description = l.Description, Order = l.Order };
    }
}
