using Application.Commands.Auth;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using SharedKernel.Interfaces;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.Auth
{
    public sealed class LogoutHandler(ISwagDbContext authDbContext, IPermissionCache permissionCache, IUserProvider userProvider) : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = ComputeHash(request.RefreshToken);
            if (!await authDbContext.Set<RefreshTokenSession>().AnyAsync(f => f.TokenHash == tokenHash && f.RevokedAt == null))
                throw new UnauthorizedAccessException("Invalid refresh token");
            await authDbContext.Set<RefreshTokenSession>().Where(f => f.TokenHash == tokenHash && f.RevokedAt == null)
                .ExecuteUpdateAsync(f => f.SetProperty(s => s.RevokedAt, DateTimeOffset.UtcNow), cancellationToken);
            await authDbContext.SaveChangesAsync(cancellationToken);
            await permissionCache.RemoveUserPermissionsAsync(userProvider.GetUserId(), cancellationToken);
            return new LogoutResponse { Success = true };
        }
        private static string ComputeHash(string value) =>
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}
