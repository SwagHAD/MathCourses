using Application.Commands.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.CreateCommands
{
    public sealed record CreateTeacherCommand : ICommand<DefaultTeacherResponse>, IMapWith<Teacher>
    {
        public string Name { get; init; }

        public string Login { get; init; }
        public string Password { get; init; }
        public int Role { get; init; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTeacherCommand, Teacher>()
                .ForMember(teacher => teacher.Name,
                    entity => entity.MapFrom(teacherdto => teacherdto.Name));
        }
    }
}
