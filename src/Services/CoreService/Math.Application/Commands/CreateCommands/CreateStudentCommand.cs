using SharedKernel.Application.Commands;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateStudentCommand : ICommand<Unit>, IMapWith<Student>
    {
        public string Name { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }
        public int Role { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateStudentCommand,Student>()
                .ForMember(studentdto => studentdto.Name,
                    entity => entity.MapFrom(student => student.Name)).ReverseMap();
        }
    }
}
