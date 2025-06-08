using MediatR;

namespace Core.MR.GroupMR.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
