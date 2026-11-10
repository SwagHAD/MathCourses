using MediatR;
using SharedKernel.Application.Commands;

namespace Application.Commands.DeleteCommands
{
    public sealed record DeleteGroupCommand : ICommand<Unit>
    {
        public long Id { get; init; }
    }
}
