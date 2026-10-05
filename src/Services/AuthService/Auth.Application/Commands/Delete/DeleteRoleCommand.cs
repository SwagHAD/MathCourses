using SharedKernel.Application.Commands;
using MediatR;

namespace Application.Commands.Delete
{
    public sealed record class DeleteRoleCommand : ICommand<Unit>
    {
        public int Id { get; init; }
    }
}
