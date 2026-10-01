using Application.Commands.Auth;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Application.Handlers.Auth
{
    public sealed class LoginHandler(
        ITokenProvider tokenProvider,
        IAuthDbContext authDbContext,
        IPasswordHasher<User> passwordHasher,
        IPermissionCache permissionCache)
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await authDbContext.Users.AsNoTracking()
                .Include(u => u.RoleRef)
                .FirstOrDefaultAsync(u => u.Login == request.Login, cancellationToken);
            if(user is null)
                throw new UnauthorizedAccessException("Неправильный логин или пароль");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result is PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Неправильный логин или пароль");

            var accessToken = tokenProvider.GenerateAccessToken(user.Id, user.RoleRef.UserType);
            var rawRefreshToken = tokenProvider.GenerateRefreshToken();
            var tokenHash = ComputeHash(rawRefreshToken);
            var expiresAt = DateTimeOffset.UtcNow.AddDays(30);
            var permissions = await authDbContext.Users.AsNoTracking()
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
            await authDbContext.RefreshTokenSessions.AddAsync(session, cancellationToken);
            await authDbContext.SaveChangesAsync();
            await permissionCache.SetUserPermissionsAsync(user.Id, permissions, cancellationToken);
            return new LoginResponse
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
