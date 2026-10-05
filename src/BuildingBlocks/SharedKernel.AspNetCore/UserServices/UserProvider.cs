using Microsoft.AspNetCore.Http;
using SharedKernel.Enums;
using SharedKernel.Interfaces;
using System.Security.Claims;

namespace SharedKernel.AspNetCore.UserServices
{
    internal sealed class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
    {
        public int GetUserId()
        {
            var claim = httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claim is null || !int.TryParse(claim, out var userId))
                throw new UnauthorizedAccessException("User is not authenticated");
            return userId;
        }
        public RoleType GetRoleType()
        {
            var claim = httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.Role)?.Value;
            if (claim is null || !Enum.TryParse<RoleType>(claim, out var roleType))
                throw new UnauthorizedAccessException("User role is not found");
            return roleType;
        }
    }
}
