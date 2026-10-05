using Application.Commands.Auth;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Auth
{
    public sealed class RefreshHandler(ISwagDbContext authDbContext, ITokenProvider tokenProvider) : IRequestHandler<RefreshCommand, RefreshResponse>
    {
        public async Task<RefreshResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = ComputeHash(request.RefreshToken);
            var session = await authDbContext.Set<RefreshTokenSession>()
            .FirstOrDefaultAsync(s =>
                s.TokenHash == tokenHash &&
                s.RevokedAt == null &&
                s.ExpiresAt > DateTimeOffset.UtcNow,
            cancellationToken);
            if (session is null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            session.RevokedAt = DateTimeOffset.UtcNow;

            var newRawRefreshToken = tokenProvider.GenerateRefreshToken();
            var newSession = new RefreshTokenSession
            {
                UserId = session.UserId,
                TokenHash = ComputeHash(newRawRefreshToken),
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(30),
                CreatedAt = DateTimeOffset.UtcNow,
            };
            await authDbContext.Set<RefreshTokenSession>().AddAsync(newSession, cancellationToken);
            await authDbContext.SaveChangesAsync(cancellationToken);
            var userType = await authDbContext.Set<User>().Where(f => f.Id == session.UserId)
                .Select(f => f.RoleRef.UserType)
                .FirstOrDefaultAsync(cancellationToken);
            return new RefreshResponse
            {
                AccessToken = tokenProvider.GenerateAccessToken(session.UserId ?? 0, userType),
                RefreshToken = newRawRefreshToken
            };
        }
        private static string ComputeHash(string value) =>
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    }
}
