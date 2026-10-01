using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.UserServices
{
    public sealed class UserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
    {
        public int GetUserId()
        {
            var claim = httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claim is null || !int.TryParse(claim, out var userId))
                throw new UnauthorizedAccessException("User is not authenticated");
            return userId;
        }
        public UserType GetUserType()
        {
            var claim = httpContextAccessor.HttpContext?.User
                .FindFirst(ClaimTypes.Role)?.Value;
            if (claim is null || !Enum.TryParse<UserType>(claim, out var userType))
                throw new UnauthorizedAccessException("User role is not found");
            return userType;
        }
    }
}
