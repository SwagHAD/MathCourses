using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Queries
{
    public class GetAllGroupsDetailsQuery : IRequest<ICollection<Group>>
    {
    }
}
