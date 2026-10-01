using Application.Interfaces;
using Application.Tools;
using Domain.Attributes;
using Domain.Enums;
using Domain.Exceptions;

namespace Auth.Api.MiddleWares
{
    public sealed class PermissionCheckerMiddleWare(IPermissionCache permissionCache, IUserProvider userProvider)
        : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            var permissionAttr = endpoint?.Metadata.GetMetadata<PermissionAttribute>();
            if (permissionAttr is null)
            {
                await next(context);
                return;
            }
            var userId = userProvider.GetUserId();
            var requiredPermission = $"{ServiceType.CoreService.ToString()}:{permissionAttr?.ObjectTypeName}:{permissionAttr?.ActionType.ToString()}";
            var haspermission = await permissionCache.HasPermissionAsync(userId, requiredPermission);
            if (!haspermission)
                throw new PermissionDeniedException(permissionAttr?.ObjectTypeName, permissionAttr?.ActionType.GetDescription());
            if(permissionAttr?.RoleType != userProvider.GetRoleType())
                throw new PermissionDeniedException(permissionAttr?.ObjectTypeName, permissionAttr?.ActionType.GetDescription());
            await next(context);
        }
    }
}
