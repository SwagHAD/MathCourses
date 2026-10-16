using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.DeleteCommands
{
    public sealed record DeleteTeacherCommand : ICommand<Unit>
    {
        public int Id { get; init; }
    }
}
