using Application.DTO.Base;
using Application.DTO.GroupDTO;
using Application.Mapping.Base;
using Domain.Entities;

namespace Application.DTO.TeacherDTO
{
    public class TeacherDto : IDTOBase<Teacher>, IMapWith<Teacher>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<GroupDto> Groups { get; set; } = [];

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Teacher, TeacherDto>()
                .ForMember(teacherdto => teacherdto.ID,
                    entity => entity.MapFrom(teacher => teacher.ID))
                .ForMember(teacherdto => teacherdto.Name,
                    entity => entity.MapFrom(teacher => teacher.Name));
        }
    }
}
