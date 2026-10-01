using Application.Commands.Base;
using MediatR;

namespace Application.Commands.Delete
{
    public sealed record DeleteUserCommand : ICommand<Unit>
    {
        public int Id { get; init; }
    }
}
