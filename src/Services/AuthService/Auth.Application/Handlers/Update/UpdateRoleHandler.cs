using Application.Commands.Update;
using Application.Services;
using AuthService.Contracts.Roles;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Cache;

namespace Application.Handlers.Update
{
    public sealed class UpdateRoleHandler(ISwagDbContext DbContext, IPermissionCacheService permissionCache) : IRequestHandler<UpdateRoleCommand, RoleResponse>
    {
        public async Task<RoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await DbContext.Set<Role>()
                .Include(x => x.RolePermissions)
                .FirstOrDefaultAsync(x => x.Id == request.Data.Id, cancellationToken) ?? throw new NotFoundException(nameof(Role), request.Data.Id);
            role.Name = request.Data.Name;
            role.RolePermissions = request.Data.Permissions
                .Select(grant => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = grant.PermissionId,
                    Scope = grant.Scope
                }).ToList();
            await DbContext.SaveChangesAsync(cancellationToken);

            // Права в кэше у всех пользователей роли сразу обновляем — иначе изменение вступит в силу только после перелогина
            var permissions = await PermissionKeys.LoadForRoleAsync(DbContext, role.Id, cancellationToken);
            var userIds = await DbContext.Set<User>().AsNoTracking()
                .Where(x => x.RoleId == role.Id)
                .Select(x => x.Id)
                .ToArrayAsync(cancellationToken);
            foreach (var userId in userIds)
                await permissionCache.SetUserPermissionsAsync(userId, permissions, cancellationToken);

            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = request.Data.Permissions
            };
        }
    }
}
