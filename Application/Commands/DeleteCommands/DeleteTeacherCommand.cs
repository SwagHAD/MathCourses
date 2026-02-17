using Application.Command.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteTeacherCommand : IDtoBaseDelete<Teacher> , IMapWith<Teacher>, IRequest<Teacher>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTeacherCommand, Teacher>()
                .ForMember(teacher => teacher.ID,
                    entity => entity.MapFrom(teacherdto => teacherdto.ID));
        }
    }
}
