using AutoMapper;
using BLL.Common.Exceptions;
using BLL.MR.GroupMR.Queries.Dto;
using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.GroupMR.Queries
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
