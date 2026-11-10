using CoreService.Contracts.Persons;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.CreateCommands
{
    public sealed record CreatePersonCommand(CreatePersonRequest Data) : ICommand<Unit>;
}
