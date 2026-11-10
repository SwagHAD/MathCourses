using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.DeleteCommands
{
    public sealed record DeleteCourseCommand : ICommand<Unit>
    {
        public long Id { get; init; }
    }
}
