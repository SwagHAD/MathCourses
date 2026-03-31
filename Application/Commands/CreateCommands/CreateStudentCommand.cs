using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public sealed class CreateStudentCommand : IRequest<DefaultStudentResponse>, IMapWith<Student>
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
