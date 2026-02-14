using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public class DeleteCourseDto : IDtoBaseDelete<Course> , IMapWith<Course>, IRequest<Course>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCourseDto, Course>()
                .ForMember(course => course.ID,
                    entity => entity.MapFrom(coursedto => coursedto.ID));
        }
    }
}
