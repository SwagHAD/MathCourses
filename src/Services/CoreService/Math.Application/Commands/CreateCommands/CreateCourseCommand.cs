using CoreService.Contracts.Courses;
using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateCourseCommand(CreateCourseRequest Data) : ICommand<Unit>;
}
