using System.Linq.Expressions;
using CoreService.Contracts.Students;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class StudentsPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Student, StudentFilter, StudentResponse>(dbContext)
    {
        protected override IQueryable<Student> ApplyFilter(IQueryable<Student> query, StudentFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(s => s.PersonRef.Name.ToLower().Contains(name));
            }
            if (filter.GroupId is int groupId)
                query = query.Where(s => s.StudentGroups.Any(g => g.GroupID == groupId));
            if (filter.HasUser is bool hasUser)
                query = query.Where(s => (s.PersonRef.UserId != null) == hasUser);
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Student, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Student, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = s => s.StudentId,
                ["name"] = s => s.PersonRef.Name,
                ["createdAt"] = s => s.CreatedAt,
            };

        protected override Expression<Func<Student, object>> DefaultSort => s => s.StudentId;

        protected override Expression<Func<Student, StudentResponse>> Projection =>
            s => new StudentResponse { Id = s.StudentId, Name = s.PersonRef.Name };
    }
}
