using CoreService.Contracts.Teachers;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.UpdateCommands
{
    public sealed record UpdateTeacherCommand(UpdateTeacherRequest Data) : ICommand<Unit>;
}
