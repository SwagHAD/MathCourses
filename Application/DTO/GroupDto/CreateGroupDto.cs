using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public sealed class CreateGroupDto : IDTOBaseCreate<Group> , IMapWith<Group>
    {
        public string Name { get; set; }
        public int? CourseID { get; set; }
        public List<int> StudentIDs { get; set; } = [];
        public List<int> TeacherIDs { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGroupDto, Group>()
                .ForMember(group => group.Name,
                    entity => entity.MapFrom(groupdto => groupdto.Name))
                .ForMember(group => group.CourseID,
                    entity => entity.MapFrom(groupdto => groupdto.CourseID))
                .ForMember(group => group.StudentGroups,
                    entity => entity.MapFrom(groupdto => groupdto.StudentIDs));
        }
    }
}
