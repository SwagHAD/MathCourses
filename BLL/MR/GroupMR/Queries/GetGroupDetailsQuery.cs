using BLL.MR.GroupMR.Queries.Dto;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.GroupMR.Queries
{
    public class GetGroupDetailsQuery : IRequest<Group>
    {
        public int Id { get; set; }
    }
}
