using Application.Commands.Update;
using Application.Responses.DefaultResponses;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;
using SharedKernel.Interfaces;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Update
{
    public sealed class UpdateRoleHandler(ISwagDbContext DbContext, IPermissionCache permissionCache) : IRequestHandler<UpdateRoleCommand, RoleResponse>
    {
        public async Task<RoleResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await DbContext.Set<Role>()
                .Include(x => x.RolePermissions)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Role), request.Id);
            role.Name = request.Name;
            role.RolePermissions = request.PermissionIds
                .Select(permissionId => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permissionId
                }).ToList();
            await DbContext.SaveChangesAsync(cancellationToken);

            var userIds = await DbContext.Set<User>().AsNoTracking()
                .Where(x => x.RoleId == role.Id)
                .Select(x => x.Id)
                .ToArrayAsync(cancellationToken);
            var permissions = await DbContext.Set<Role>().AsNoTracking()
                .Where(x => x.Id == role.Id)
                .SelectMany(x => x.RolePermissions)
                .Select(x => $"{x.PermissionRef.ObjectTypeRef.ServiceType.ToString()}:{x.PermissionRef.ObjectType}:{x.PermissionRef.ActionType.ToString()}")
                .ToArrayAsync(cancellationToken);
            if (userIds.Any())
            {
                foreach (var userId in userIds)
                {
                    await permissionCache.SetUserPermissionsAsync(userId, permissions);
                }
            }    
            return new RoleResponse
            {
                Id = role.Id,
                Name = role.Name,
                PermissionIds = request.PermissionIds.ToArray()
            };
        }
    }
}
