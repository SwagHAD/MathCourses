using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Queries
{
    public class GetAllGroupsDetailsQueryHandler(IGenericRepository<Group> groupRepository) : IRequestHandler<GetAllGroupsDetailsQuery, ICollection<Group>>
    {
        public async Task<ICollection<Group>> Handle(GetAllGroupsDetailsQuery request, CancellationToken cancellationToken)
        {
            return await groupRepository.GetAllAsync(cancellationToken);
        }
    }
}
