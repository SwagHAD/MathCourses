using Application.Commands.CreateCommands;
using Application.Commands.DeleteCommands;
using Application.Commands.UpdateCommands;
using Application.Queries.DefaultQueries;
using CoreService.Contracts;
using CoreService.Contracts.Persons;
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
    public sealed class PersonController(IMediator mediator) : ControllerBase
    {
        [HttpGet(CoreRoutes.Persons.Get)]
        [Permission(ObjectTypeName = nameof(Person), ActionType = ActionType.Read)]
        public async Task<ActionResult<PersonDetailsResponse>> GetPerson([FromQuery] GetPersonQuery query, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet(CoreRoutes.Persons.GetPage)]
        [Permission(ObjectTypeName = nameof(Person), ActionType = ActionType.Read)]
        public async Task<ActionResult<PageResult<PersonResponse>>> GetPersons([FromQuery] PageRequest<PersonFilter> request,
            CancellationToken cancellationToken)
        {
            return await mediator.Send(new PaginationQuery<PersonFilter, PersonResponse>(request), cancellationToken);
        }

        [HttpGet(CoreRoutes.Persons.Count)]
        [Permission(ObjectTypeName = nameof(Person), ActionType = ActionType.Read)]
        public async Task<ActionResult<int>> GetCountOfPersons(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountOfPersonsQuery(), cancellationToken);
        }

        [HttpPost(CoreRoutes.Persons.Create)]
        [Permission(ObjectTypeName = nameof(Person), ActionType = ActionType.Create)]
        public async Task<ActionResult> CreatePerson([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new CreatePersonCommand(request), cancellationToken));
        }

        [HttpPut(CoreRoutes.Persons.Update)]
        [Permission(ObjectTypeName = nameof(Person), ActionType = ActionType.Update)]
        public async Task<ActionResult> UpdatePerson([FromBody] UpdatePersonRequest request, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(new UpdatePersonCommand(request), cancellationToken));
        }

        [HttpDelete(CoreRoutes.Persons.Delete)]
        [Permission(ObjectTypeName = nameof(Person), ActionType = ActionType.Delete)]
        public async Task<ActionResult> DeletePerson([FromQuery] DeletePersonCommand command, CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(command, cancellationToken));
        }
    }
}
