using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using CoreService.Contracts;
using CoreService.Contracts.Groups;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application.Pagination;
using SharedKernel.Attributes;
using SharedKernel.Contracts.Pagination;
using SharedKernel.Enums;

namespace Math.Api.Controllers
{
    [Authorize]
    [Route(CoreRoutes.Prefix)]
    public sealed class GroupController(IMediator mediator) : ControllerBase
    {
        [HttpGet(CoreRoutes.Groups.Get)]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Read)]
        public async Task<ActionResult<GroupDetailsResponse>> GetGroup([FromQuery] GetGroupQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(CoreRoutes.Groups.GetPage)]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<GroupResponse>>> GetGroups([FromQuery] PageRequest<GroupFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<GroupFilter, GroupResponse>(request), cancellationToken);
        }

        [HttpGet(CoreRoutes.Groups.Count)]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfGroups(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountOfGroupsQuery(), cancellationToken);
        }

        [HttpPost(CoreRoutes.Groups.Create)]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreateGroup([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new CreateGroupCommand(request), cancellationToken));
        }

        [HttpPut(CoreRoutes.Groups.Update)]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdateGroup([FromBody] UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new UpdateGroupCommand(request), cancellationToken));
        }

        [HttpDelete(CoreRoutes.Groups.Delete)]
        [Permission(ObjectTypeName = nameof(Group), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeleteGroup([FromQuery] DeleteGroupCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }
    }
}
