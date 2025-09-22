using Application.DTO.Base;
using Application.DTO.StudentDTO;
using Application.Mapping.Base;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public class GroupDto : IDataTransferObjectBase<Group>, IMapWith<Group>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? TeacherID { get; set; }

        public int? CourseID { get; set; }

        public List<StudentDto> Students { get; set; } = [];

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Group, GroupDto>()
                .ForMember(groupdto => groupdto.ID,
                    entity => entity.MapFrom(group => group.ID))
                .ForMember(groupdto => groupdto.Name,
                    entity => entity.MapFrom(group => group.Name))
                .ForMember(groupdto => groupdto.TeacherID,
                    entity => entity.MapFrom(group => group.TeacherID))
                .ForMember(groupdto => groupdto.CourseID,
                    entity => entity.MapFrom(group => group.CourseID));
        }
    }
}
