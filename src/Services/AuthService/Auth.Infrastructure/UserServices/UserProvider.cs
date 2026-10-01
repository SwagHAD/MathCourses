using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrasctrure.UserServices
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
            if (claim is null || !Enum.TryParse<RoleType>(claim, out var roletype))
                throw new UnauthorizedAccessException("User is not authenticated");
            return roletype;
        }
    }
}
