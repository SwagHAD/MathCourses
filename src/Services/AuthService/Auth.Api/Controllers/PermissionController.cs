using Application.Queries.DefaultQueries;
using AuthService.Contracts;
using AuthService.Contracts.Permissions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application.Pagination;
using SharedKernel.Attributes;
using SharedKernel.Contracts.Pagination;
using SharedKernel.Enums;

namespace Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route(AuthRoutes.Prefix)]
    public sealed class PermissionController(IMediator mediator) : ControllerBase
    {
        [HttpGet(AuthRoutes.Permissions.Get)]
        [Permission(ObjectTypeName = nameof(Permission), ActionType = ActionType.Read)]
        public async Task<ActionResult<PermissionResponse>> GetPermission([FromQuery] GetPermissionQuery query,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(AuthRoutes.Permissions.GetPage)]
        [Permission(ObjectTypeName = nameof(Permission), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<PermissionResponse>>> GetPermissions([FromQuery] PageRequest<PermissionFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<PermissionFilter, PermissionResponse>(request), cancellationToken);
        }
    }
}
