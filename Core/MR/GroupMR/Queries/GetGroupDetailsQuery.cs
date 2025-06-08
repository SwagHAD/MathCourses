using DataMath.Entities;
using MediatR;

namespace Core.MR.GroupMR.Queries
{
    public class GetGroupDetailsQuery : IRequest<Group>
    {
        public int Id { get; set; }
    }
}
