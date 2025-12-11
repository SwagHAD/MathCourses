using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.GroupDTO
{
    public class CreateGroupDto : IDataTransferObjectBaseCreate<Group> , IMapWith<Group>
    {
        public string Name { get; set; }

        public int? TeacherID {  get; set; }

        public int? CourseID { get; set; }

        public List<int> Students { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGroupDto, Group>()
                .ForMember(group => group.Name,
                    entity => entity.MapFrom(groupdto => groupdto.Name))
                .ForMember(group => group.TeacherID,
                    entity => entity.MapFrom(groupdto => groupdto.TeacherID))
                .ForMember(group => group.CourseID,
                    entity => entity.MapFrom(groupdto => groupdto.CourseID))
                .ForMember(group => group.StudentGroups,
                    entity => entity.MapFrom(groupdto => groupdto.Students));
        }
    }
}
