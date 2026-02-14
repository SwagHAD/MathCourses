using Application.DTO.Base;
using Application.Mapping.Base;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public class UpdateCourseDto : IDtoBaseUpdate<Course> , IMapWith<Course>, IRequest<Course>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<UpdateCourseDto, Course>()
                .ForMember(course => course.ID,
                    entity => entity.MapFrom(coursedto => coursedto.ID))
                .ForMember(course => course.Name,
                    entity => entity.MapFrom(coursedto => coursedto.Name));
        }
    }
}
