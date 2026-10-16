using CoreService.Contracts.Groups;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateGroupCommand(CreateGroupRequest Data) : ICommand<Unit>;
}
