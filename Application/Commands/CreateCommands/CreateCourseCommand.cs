using Application.Command.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.CreateCommands
{
    public sealed class CreateCourseCommand : IBaseRequestCreate<Course>, IMapWith<Course>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCourseCommand, Course>()
                .ForMember(course => course.Name,
                    entity => entity.MapFrom(coursedto => coursedto.Name));
        }
    }
}
