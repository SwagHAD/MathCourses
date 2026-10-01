using Application.Interfaces;
using Application.Tools;
using Domain.Attributes;
using Domain.Enums;
using Domain.Exceptions;

namespace Math.Api.MiddleWares
{
    public sealed class PermissionCheckerMiddleWare(IUserProvider userProvider, IPermissionCache permissionCache) : IMiddleware
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
            await next(context);
        }
    }
}
