using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SharedKernel.Access;
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
        /// <summary>Ключ в HttpContext.Items: область действия права для текущего эндпоинта (<see cref="Contracts.Access.AccessScope"/>).</summary>
        public const string ScopeItemKey = "permission-scope";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            var permissionAttr = endpoint?.Metadata.GetMetadata<PermissionAttribute>();
            if (permissionAttr is null)
            {
                await next(context);
                return;
            }
            // К эндпоинту пускаем при праве в любой области; какие записи видны — решает фильтр данных по области
            var basePermission = PermissionKey.Base(serviceOptions.Value.ServiceType, permissionAttr.ObjectTypeName, permissionAttr.ActionType);
            var scope = await permissionCache.GetScopeAsync(basePermission, context.RequestAborted)
                ?? throw new PermissionDeniedException(permissionAttr.ObjectTypeName, permissionAttr.ActionType.GetDescription());
            context.Items[ScopeItemKey] = scope;
            await next(context);
        }
    }
}
