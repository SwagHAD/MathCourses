using Application.Commands.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public sealed class CreateLessonCommand : ICommand<Unit>, IMapWith<Lesson>
    {
        public string Name { get; set; }

        public int? GroupID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateLessonCommand, Lesson>()
                .ForMember(lessondto => lessondto.Name,
                    entity => entity.MapFrom(lessondto => lessondto.Name))
                .ForMember(lessondto => lessondto.GroupID,
                    entity => entity.MapFrom(lessondto => lessondto.GroupID));
        }
    }
}
