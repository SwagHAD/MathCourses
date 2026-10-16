using CoreService.Contracts.Lessons;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateLessonCommand(CreateLessonRequest Data) : ICommand<Unit>;
}
