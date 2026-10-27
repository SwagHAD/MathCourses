using Application.Commands.Auth;
using Application.Interfaces;
using AuthService.Contracts.Auth;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using SharedKernel.Enums;
using SharedKernel.Application.Interfaces;
using SharedKernel.Cache;

namespace Application.Handlers.Auth
{
    public sealed class LoginHandler(
        ITokenProvider tokenProvider,
        ISwagDbContext authDbContext,
        IPasswordHasher<User> passwordHasher,
        IPermissionCacheService permissionCache)
        : IRequestHandler<LoginCommand, TokenResponse>
    {
        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await authDbContext.Set<User>().AsNoTracking()
                .Include(u => u.RoleRef)
                .FirstOrDefaultAsync(u => u.Login == request.Data.Login, cancellationToken);
            if(user is null)
                throw new UnauthorizedAccessException("Неправильный логин или пароль");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Data.Password);
            if (result is PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Неправильный логин или пароль");

            var accessToken = tokenProvider.GenerateAccessToken(user.Id);
            var rawRefreshToken = tokenProvider.GenerateRefreshToken();
            var tokenHash = ComputeHash(rawRefreshToken);
            var expiresAt = DateTimeOffset.UtcNow.AddDays(30);
            var permissions = await authDbContext.Set<User>().AsNoTracking()
                .Where(u => u.Id == user.Id)
                .Select(u => u.RoleRef)
                .SelectMany(r => r.RolePermissions)
                .Select(rp => $"{rp.PermissionRef.ObjectTypeRef.ServiceType.ToString()}:{rp.PermissionRef.ObjectType.ToString()}:{rp.PermissionRef.ActionType.ToString()}")
                .ToArrayAsync(cancellationToken);

            var session = new RefreshTokenSession
            {
                UserId = user.Id,
                TokenHash = tokenHash,
                ExpiresAt = expiresAt,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            await authDbContext.Set<RefreshTokenSession>().AddAsync(session, cancellationToken);
            await authDbContext.SaveChangesAsync();
            await permissionCache.SetUserPermissionsAsync(user.Id, permissions, cancellationToken);
            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = rawRefreshToken,
            };
        }
        private static string ComputeHash(string value)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
        }
    }
}
