using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharedKernel.AspNetCore.Options;
using SharedKernel.Attributes;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;
using SharedKernel.Tools;

namespace SharedKernel.AspNetCore.MiddleWares
{
    public sealed class PermissionCheckerMiddleWare(IUserProvider userProvider, IPermissionCache permissionCache,
        IOptions<ServiceOptions> serviceOptions) : IMiddleware
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
            var requiredPermission = $"{serviceOptions.Value.ServiceType}:{permissionAttr.ObjectTypeName}:{permissionAttr.ActionType}";
            var hasPermission = await permissionCache.HasPermissionAsync(userId, requiredPermission, context.RequestAborted);
            if (!hasPermission)
                throw new PermissionDeniedException(permissionAttr.ObjectTypeName, permissionAttr.ActionType.GetDescription());
            if (permissionAttr.Roles.Count > 0 && !permissionAttr.Roles.Contains(userProvider.GetRoleType()))
                throw new PermissionDeniedException(permissionAttr.ObjectTypeName, permissionAttr.ActionType.GetDescription());
            await next(context);
        }
    }
}
