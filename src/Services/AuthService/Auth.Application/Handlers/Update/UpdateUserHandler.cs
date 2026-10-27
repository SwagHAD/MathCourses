using Application.Commands.Update;
using AuthService.Contracts.Users;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Enums;
using SharedKernel.Application.Interfaces;
using SharedKernel.Cache;

namespace Application.Handlers.Update
{
    public sealed class UpdateUserHandler(ISwagDbContext DbContext, IPermissionCacheService permissionCache,
        IPasswordHasher<User> passwordHasher) : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await DbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == request.Data.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Data.Id);

            user.Login = request.Data.Login;
            // Раньше сюда записывалась строка от клиента как есть — после этого вход ломался
            if (!string.IsNullOrEmpty(request.Data.Password))
                user.PasswordHash = passwordHasher.HashPassword(user, request.Data.Password);
            user.RoleId = request.Data.RoleId;

            await DbContext.SaveChangesAsync(cancellationToken);
            var permissions = await DbContext.Set<User>().AsNoTracking()
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
                RoleId = request.Data.RoleId
            };
        }
    }
}
