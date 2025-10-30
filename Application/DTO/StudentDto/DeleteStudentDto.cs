using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.StudentDTO
{
    public class DeleteStudentDto : IDataTransferObjectBaseDelete<Student>, IMapWith<Student>
    {
        public int? ID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteStudentDto, Student>()
                .ForMember(student => student.ID,
                    entity => entity.MapFrom(studentdto => studentdto.ID));
        }
    }
}
