using Application.Commands.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteGroupCommand : ICommand<DefaultGroupResponse> , IMapWith<Group>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteGroupCommand, Group>()
                .ForMember(group => group.ID,
                    entity => entity.MapFrom(groupdto => groupdto.ID));
        }
    }
}
