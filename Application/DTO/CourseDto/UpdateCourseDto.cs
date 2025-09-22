using Application.DTO.Base;
using Application.Mapping.Base;
using Domain.Entities;

namespace Application.DTO.CourseDTO
{
    public class UpdateCourseDto : IDataTransferObjectBase<Course> , IMapWith<Course>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<UpdateCourseDto, Course>()
                .ForMember(course => course.ID,
                    entity => entity.MapFrom(coursedto => coursedto.Id))
                .ForMember(course => course.Name,
                    entity => entity.MapFrom(coursedto => coursedto.Name));
        }
    }
}
