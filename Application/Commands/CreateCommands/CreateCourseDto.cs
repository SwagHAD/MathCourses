using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public class CreateCourseDto : IDtoBaseCreate<Course>, IMapWith<Course>, IRequest<Course>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCourseDto, Course>()
                .ForMember(course => course.Name,
                    entity => entity.MapFrom(coursedto => coursedto.Name));
        }
    }
}
