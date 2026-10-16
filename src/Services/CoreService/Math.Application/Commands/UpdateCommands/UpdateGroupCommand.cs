using CoreService.Contracts.Groups;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.UpdateCommands
{
    public sealed record UpdateGroupCommand(UpdateGroupRequest Data) : ICommand<Unit>;
}
