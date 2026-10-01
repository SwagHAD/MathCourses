using Application.Commands.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public sealed class CreateCourseCommand : ICommand<Unit>, IMapWith<Course>
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
