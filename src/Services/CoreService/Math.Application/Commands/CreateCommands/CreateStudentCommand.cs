using CoreService.Contracts.Students;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateStudentCommand(CreateStudentRequest Data) : ICommand<Unit>;
}
