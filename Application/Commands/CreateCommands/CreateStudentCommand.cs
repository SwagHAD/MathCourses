using Application.Command.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public class CreateStudentCommand : IBaseRequestCreate<Student>, IMapWith<Student>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStudentCommand,Student>()
                .ForMember(studentdto => studentdto.Name,
                    entity => entity.MapFrom(student => student.Name)).ReverseMap();
        }
    }
}
