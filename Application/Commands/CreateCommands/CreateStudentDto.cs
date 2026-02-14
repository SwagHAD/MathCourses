using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public class CreateStudentDto : IDtoBaseCreate<Student>, IMapWith<Student>, IRequest<Student>
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
