using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public class DeleteStudentDto : IDtoBaseDelete<Student>, IMapWith<Student>, IRequest<Student>
    {
        public int ID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteStudentDto, Student>()
                .ForMember(student => student.ID,
                    entity => entity.MapFrom(studentdto => studentdto.ID));
        }
    }
}
