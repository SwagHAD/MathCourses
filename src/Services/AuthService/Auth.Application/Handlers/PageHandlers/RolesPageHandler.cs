using System.Linq.Expressions;
using AuthService.Contracts.Roles;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class RolesPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Role, RoleFilter, RoleResponse>(dbContext)
    {
        protected override IQueryable<Role> ApplyFilter(IQueryable<Role> query, RoleFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(r => r.Name.ToLower().Contains(name));
            }
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Role, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Role, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = r => r.Id,
                ["name"] = r => r.Name,
                ["createdAt"] = r => r.CreatedAt,
            };

        protected override Expression<Func<Role, object>> DefaultSort => r => r.Id;

        protected override Expression<Func<Role, RoleResponse>> Projection =>
            r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name,
                PermissionIds = r.RolePermissions.Select(rp => rp.PermissionId).ToArray()
            };
    }
}
