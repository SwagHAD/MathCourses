using Application.Commands.Create;
using Application.Commands.Delete;
using Application.Commands.Update;
using Application.Queries.DefaultQueries;
using AuthService.Contracts;
using AuthService.Contracts.Roles;
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
    public sealed class RoleController(IMediator mediator) : ControllerBase
    {
        [HttpGet(AuthRoutes.Roles.Get)]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Read)]
        public async Task<ActionResult<RoleResponse>> GetRole([FromQuery] GetRoleQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(AuthRoutes.Roles.GetPage)]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<RoleResponse>>> GetRoles([FromQuery] PageRequest<RoleFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<RoleFilter, RoleResponse>(request), cancellationToken);
        }

        [HttpPost(AuthRoutes.Roles.Create)]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Create)]
        public async Task<ActionResult<int>> CreateRole([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateRoleCommand(request), cancellationToken);
        }

        [HttpPut(AuthRoutes.Roles.Update)]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Update)]
        public async Task<ActionResult<RoleResponse>> UpdateRole([FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateRoleCommand(request), cancellationToken);
        }

        [HttpDelete(AuthRoutes.Roles.Delete)]
        [Permission(ObjectTypeName = nameof(Role), ActionType = ActionType.Delete)]
        public async Task<ActionResult<Unit>> DeleteRole([FromQuery] DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }
    }
}
