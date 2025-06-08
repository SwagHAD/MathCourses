using BLL.Common.Exceptions;
using Core.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Queries
{
    public class GetGroupDetailsQueryHandler(IGenericRepository<Group> groupRepository) : IRequestHandler<GetGroupDetailsQuery, Group>
    {
        public async Task<Group> Handle(GetGroupDetailsQuery request, CancellationToken cancellationToken)
        {
            var group = await groupRepository.GetByIdAsync(request.Id, cancellationToken);

            if (group is null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            return group;
        }

    }
}
