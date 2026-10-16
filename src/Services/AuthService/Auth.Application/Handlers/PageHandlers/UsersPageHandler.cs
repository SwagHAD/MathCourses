using System.Linq.Expressions;
using AuthService.Contracts.Users;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class UsersPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<User, UserFilter, UserResponse>(dbContext)
    {
        protected override IQueryable<User> ApplyFilter(IQueryable<User> query, UserFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Login))
            {
                var login = filter.Login.Trim().ToLower();
                query = query.Where(u => u.Login.ToLower().Contains(login));
            }
            if (filter.RoleId is int roleId)
                query = query.Where(u => u.RoleId == roleId);
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<User, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<User, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = u => u.Id,
                ["login"] = u => u.Login,
                ["createdAt"] = u => u.CreatedAt,
            };

        protected override Expression<Func<User, object>> DefaultSort => u => u.Id;

        // PasswordHash наружу не отдаём: в проекции только безопасные поля
        protected override Expression<Func<User, UserResponse>> Projection =>
            u => new UserResponse { Id = u.Id, Login = u.Login, RoleId = u.RoleId };
    }
}
