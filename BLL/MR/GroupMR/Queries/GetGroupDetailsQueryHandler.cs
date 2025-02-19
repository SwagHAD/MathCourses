using AutoMapper;
using BLL.Common.Exceptions;
using BLL.MR.GroupMR.Queries.Dto;
using BLL.Repository;
using DataMath.Entities;
using MediatR;

namespace BLL.MR.GroupMR.Queries
{
    public class GetGroupDetailsQueryHandler(IGenericRepository<Group> groupRepository, IMapper mapper) : IRequestHandler<GetGroupDetailsQuery, GroupDetailsDto>
    {
        public async Task<GroupDetailsDto> Handle(GetGroupDetailsQuery request, CancellationToken cancellationToken)
        {
            var group = await groupRepository.GetByIdAsync(request.Id, cancellationToken);

            if (group is null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            return mapper.Map<GroupDetailsDto>(group); 
        }

    }
}
