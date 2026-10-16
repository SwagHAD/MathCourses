using CoreService.Contracts.Teachers;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateTeacherCommand(CreateTeacherRequest Data) : ICommand<Unit>;
}
