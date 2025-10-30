using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.CourseDTO
{
    public class DeleteCourseDto : IDataTransferObjectBaseDelete<Course> , IMapWith<Course>
    {
        public int? ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCourseDto, Course>()
                .ForMember(course => course.ID,
                    entity => entity.MapFrom(coursedto => coursedto.ID));
        }
    }
}
