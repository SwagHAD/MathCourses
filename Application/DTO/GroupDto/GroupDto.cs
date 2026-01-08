using Application.DTO.Base;
using Application.DTO.StudentDTO;
using Application.Mapping.Base;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public sealed class GroupDto : IDTOBase<Group>, IMapWith<Group>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<int> TeacherIDs { get; set; } = [];
        public int? CourseID { get; set; }
        public List<int> StudentIDs { get; set; } = [];

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Group, GroupDto>()
                .ForMember(groupdto => groupdto.ID,
                    entity => entity.MapFrom(group => group.ID))
                .ForMember(groupdto => groupdto.Name,
                    entity => entity.MapFrom(group => group.Name))
                .ForMember(groupdto => groupdto.CourseID,
                    entity => entity.MapFrom(group => group.CourseID));
        }
    }
}
