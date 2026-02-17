using Application.Command.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateTeacherCommand : IDtoBaseUpdate<Teacher>, IMapWith<Teacher>, IRequest<Teacher>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<int> Groups { get; set; } = [];
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTeacherCommand, Teacher>()
                .ForMember(teacher => teacher.ID,
                    entity => entity.MapFrom(teacherdto => teacherdto.ID))
                .ForMember(teacher => teacher.Name,
                    entity => entity.MapFrom(teacherdto => teacherdto.Name));
        }
    }
}
