using CoreService.Contracts.Courses;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.UpdateCommands
{
    public sealed record UpdateCourseCommand(UpdateCourseRequest Data) : ICommand<Unit>;
}
