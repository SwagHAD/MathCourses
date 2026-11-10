using Application.Commands.Update;
using Application.Services;
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
            var permissions = await PermissionKeys.LoadForRoleAsync(DbContext, user.RoleId, cancellationToken);
            await permissionCache.SetUserPermissionsAsync(user.Id, permissions, cancellationToken);
            return new UserResponse
            {
                Id = user.Id,
                Login = user.Login,
                RoleId = request.Data.RoleId
            };
        }
    }
}
