using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.DeleteCommands
{
    public sealed record DeleteCourseCommand : ICommand<Unit>
    {
        public int Id { get; init; }
    }
}
