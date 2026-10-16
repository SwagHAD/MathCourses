using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.DeleteCommands
{
    public sealed record DeleteStudentCommand : ICommand<Unit>
    {
        public int Id { get; init; }
    }
}
