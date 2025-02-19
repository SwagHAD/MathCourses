using BLL.Common.Exceptions;
using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.GroupMR.Commands.UpdateGroup
{
    public class UpdateGroupCommandHandler(IGenericRepository<Group> groupRepository) : IRequestHandler<UpdateGroupCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await groupRepository.GetByIdAsync(request.Id, cancellationToken);
            if (group is null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }
            group.Name = request.Name;
            group.TeacherId = request.TeacherId;
            group.Students = request.Students;

            await groupRepository.UpdateAsync(group, cancellationToken);
            return Unit.Value;
        }
    }
}
