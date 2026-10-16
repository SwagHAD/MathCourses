using CoreService.Contracts.Students;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.UpdateCommands
{
    public sealed record UpdateStudentCommand(UpdateStudentRequest Data) : ICommand<Unit>;
}
