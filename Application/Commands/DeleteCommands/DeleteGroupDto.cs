using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public class DeleteGroupDto : IDtoBaseDelete<Group> , IMapWith<Group>, IRequest<Group>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteGroupDto, Group>()
                .ForMember(group => group.ID,
                    entity => entity.MapFrom(groupdto => groupdto.ID));
        }
    }
}
