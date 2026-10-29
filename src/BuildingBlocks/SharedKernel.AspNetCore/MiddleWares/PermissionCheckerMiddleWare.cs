using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharedKernel.AspNetCore.Options;
using SharedKernel.Attributes;
using SharedKernel.Cache;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace SharedKernel.AspNetCore.MiddleWares
{
    public sealed class PermissionCheckerMiddleWare(IPermissionCacheService permissionCache,
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
            var requiredPermission = $"{serviceOptions.Value.ServiceType}:{permissionAttr.ObjectTypeName}:{permissionAttr.ActionType}:{permissionAttr.TypeAcceses}";
            var hasPermission = await permissionCache.HasPermissionAsync(requiredPermission, context.RequestAborted);
            if (!hasPermission)
                throw new PermissionDeniedException(permissionAttr.ObjectTypeName, permissionAttr.ActionType.GetDescription());
            await next(context);
        }
    }
}
