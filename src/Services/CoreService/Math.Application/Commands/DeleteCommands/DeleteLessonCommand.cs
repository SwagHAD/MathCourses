using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.DeleteCommands
{
    public sealed record DeleteLessonCommand : ICommand<Unit>
    {
        public int Id { get; init; }
    }
}
