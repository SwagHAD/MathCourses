using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.TeacherDTO
{
    public class CreateTeacherDto : IDataTransferObjectBaseCreate<Teacher>, IMapWith<Teacher>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTeacherDto, Teacher>()
                .ForMember(teacher => teacher.Name,
                    entity => entity.MapFrom(teacherdto => teacherdto.Name));
        }
    }
}
