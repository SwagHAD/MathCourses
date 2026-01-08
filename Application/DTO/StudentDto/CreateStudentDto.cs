using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.StudentDTO
{
    public class CreateStudentDto : IDTOBaseCreate<Student>, IMapWith<Student>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStudentDto,Student>()
                .ForMember(studentdto => studentdto.Name,
                    entity => entity.MapFrom(student => student.Name)).ReverseMap();
        }
    }
}
