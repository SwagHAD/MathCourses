using Application.Queries.DefaultQueries;
using AuthService.Contracts;
using AuthService.Contracts.Users;
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
    public sealed class UserController(IMediator mediator) : ControllerBase
    {
        [HttpGet(AuthRoutes.Users.Get)]
        [Permission(ObjectTypeName = nameof(User), ActionType = ActionType.Read)]
        public async Task<ActionResult<UserResponse>> GetUser([FromQuery] GetUserQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(AuthRoutes.Users.GetPage)]
        [Permission(ObjectTypeName = nameof(User), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<UserResponse>>> GetUsers([FromQuery] PageRequest<UserFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<UserFilter, UserResponse>(request), cancellationToken);
        }

        /// <summary>Доступы текущего пользователя из кэша.</summary>
        [HttpGet(AuthRoutes.Users.CurrentPermissions)]
        public async Task<ActionResult<IReadOnlySet<string>>> GetPermissions(CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new GetUserPermissionsQuery(), cancellationToken));
        }
    }
}
