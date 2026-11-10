using Application.Commands.AccessCommands;
using CoreService.Contracts;
using Domain.Access;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Math.Api.Controllers
{
    [Authorize]
    [Route(CoreRoutes.Prefix)]
    public sealed class AccessController(IMediator mediator) : ControllerBase
    {
        /// <summary>Возвращает число созданных кортежей.</summary>
        [HttpPost(CoreRoutes.Access.Rebuild)]
        [Permission(ObjectTypeName = nameof(AccessTuple), ActionType = ActionType.Update)]
        public async Task<ActionResult<int>> RebuildAccessTuples(CancellationToken cancellationToken)
        {
            return await mediator.Send(new RebuildAccessTuplesCommand(), cancellationToken);
        }
    }
}
