using Application.Commands.Update;
using Application.Interfaces;
using Application.Responses.DefaultResponses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Update
{
    public sealed class UpdateUserHandler(IAuthDbContext DbContext, IPermissionCache permissionCache) : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Id);

            user.Login = request.Login;
            user.PasswordHash = request.PasswordHash;
            user.RoleId = request.RoleId;

            await DbContext.SaveChangesAsync(cancellationToken);
            var permissions = await DbContext.Users.AsNoTracking()
                .Where(u => u.Id == user.Id)
                .Select(u => u.RoleRef)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => $"{rp.PermissionRef.ObjectTypeRef.ServiceType.ToString()}:{rp.PermissionRef.ObjectType.ToString()}:{rp.PermissionRef.ActionType.ToString()}")
                .Distinct()
                .ToArrayAsync(cancellationToken);
            await permissionCache.SetUserPermissionsAsync(user.Id, permissions);
            return new UserResponse
            {
                Id = user.Id,
                Login = user.Login,
                Role = request.RoleId
            };
        }
    }
}
