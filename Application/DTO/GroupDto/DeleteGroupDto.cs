using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public class DeleteGroupDto : IDataTransferObjectBase<Group> , IMapWith<Group>
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteGroupDto, Group>()
                .ForMember(group => group.ID,
                    entity => entity.MapFrom(groupdto => groupdto.Id));
        }
    }
}
