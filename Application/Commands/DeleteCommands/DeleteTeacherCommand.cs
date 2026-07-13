using Application.Commands.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteTeacherCommand : ICommand<DefaultTeacherResponse>,IMapWith<Teacher>
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
