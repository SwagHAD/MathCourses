using CoreService.Contracts.Lessons;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.UpdateCommands
{
    public sealed record UpdateLessonCommand(UpdateLessonRequest Data) : ICommand<Unit>;
}
