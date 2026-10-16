using System.Linq.Expressions;
using AuthService.Contracts.Permissions;
using Domain.Entities;
using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Pagination;

namespace Application.Handlers.PageHandlers
{
    internal sealed class PermissionsPageHandler(ISwagDbContext dbContext)
        : PaginationHandler<Permission, PermissionFilter, PermissionResponse>(dbContext)
    {
        protected override IQueryable<Permission> ApplyFilter(IQueryable<Permission> query, PermissionFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var name = filter.Name.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(filter.ObjectType))
                query = query.Where(p => p.ObjectType == filter.ObjectType);
            return query;
        }

        protected override IReadOnlyDictionary<string, Expression<Func<Permission, object>>> SortKeys { get; } =
            new Dictionary<string, Expression<Func<Permission, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = p => p.Id,
                ["name"] = p => p.Name,
                ["objectType"] = p => p.ObjectType,
            };

        protected override Expression<Func<Permission, object>> DefaultSort => p => p.Id;

        protected override Expression<Func<Permission, PermissionResponse>> Projection =>
            p => new PermissionResponse { Id = p.Id, Name = p.Name };
    }
}
