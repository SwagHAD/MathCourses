using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.StudentDto
{
    public class CreateStudentDto : IDataTransferObjectBase<Student>, IMapWith<Student>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, CreateStudentDto>()
                .ForMember(studentdto => studentdto.Name,
                    entity => entity.MapFrom(student => student.Name));
        }
    }
}
