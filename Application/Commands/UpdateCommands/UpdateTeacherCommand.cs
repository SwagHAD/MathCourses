using Application.Commands.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateTeacherCommand : ICommand<Unit>, IMapWith<Teacher>
    {
        public int ID { get; set; }
        public string Name { get; set; }
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
