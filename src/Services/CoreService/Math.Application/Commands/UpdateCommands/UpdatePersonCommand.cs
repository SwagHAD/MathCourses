using CoreService.Contracts.Persons;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.UpdateCommands
{
    public sealed record UpdatePersonCommand(UpdatePersonRequest Data) : ICommand<Unit>;
}
