using Application.Commands.Auth;
using Application.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Application.Handlers.Auth
{
    public sealed class LogoutHandler(IAuthDbContext authDbContext, IPermissionCache permissionCache, IUserProvider userProvider) : IRequestHandler<LogoutCommand, LogoutResponse>
    {
        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = ComputeHash(request.RefreshToken);
            if (!await authDbContext.RefreshTokenSessions.AnyAsync(f => f.TokenHash == tokenHash && f.RevokedAt == null))
                throw new UnauthorizedAccessException("Invalid refresh token");
            await authDbContext.RefreshTokenSessions.Where(f => f.TokenHash == tokenHash && f.RevokedAt == null)
                .ExecuteUpdateAsync(f => f.SetProperty(s => s.RevokedAt, DateTimeOffset.UtcNow), cancellationToken);
            await authDbContext.SaveChangesAsync(cancellationToken);
            await permissionCache.RemoveUserPermissionsAsync(userProvider.GetUserId(), cancellationToken);
            return new LogoutResponse { Success = true };
        }
        private static string ComputeHash(string value) =>
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}
