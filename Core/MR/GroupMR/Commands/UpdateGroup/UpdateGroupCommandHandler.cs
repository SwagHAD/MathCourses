using BLL.Common.Exceptions;
using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Commands.UpdateGroup
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
            group.TeacherId = request.Teacher.Id;
            group.Students = request.Students;

            await groupRepository.UpdateAsync(group, cancellationToken);
            return Unit.Value;
        }
    }
}
