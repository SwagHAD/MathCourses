using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.StudentDTO
{
    public class StudentDto : IDTOBase<Student>, IMapWith<Student>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDto>()
                .ForMember(studentdto => studentdto.ID,
                    entity => entity.MapFrom(student => student.ID))
                .ForMember(studentdto => studentdto.Name,
                    entity => entity.MapFrom(student => student.Name));
        }
    }
}
