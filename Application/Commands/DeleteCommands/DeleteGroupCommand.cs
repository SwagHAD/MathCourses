using Application.Command.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteGroupCommand : IBaseRequestDelete<DefaultGroupResponse> , IMapWith<Group>, IRequest<Group>
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
