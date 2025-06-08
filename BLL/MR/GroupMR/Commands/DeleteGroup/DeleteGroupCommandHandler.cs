using BLL.Common.Exceptions;
using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler(IGenericRepository<Group> groupRepository) : IRequestHandler<DeleteGroupCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await groupRepository.GetByIdAsync(request.Id, cancellationToken);
            if(group is null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            await groupRepository.DeleteAsync(group, cancellationToken);
            return Unit.Value;
        }
    }
}
