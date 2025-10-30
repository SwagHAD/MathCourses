using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.CourseDTO
{
    public class CreateCourseDto : IDataTransferObjectBaseCreate<Course>, IMapWith<Course>
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
