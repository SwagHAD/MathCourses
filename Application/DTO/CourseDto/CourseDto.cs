using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.CourseDTO
{
    public class CourseDto : IDataTransferObjectBase<Course>, IMapWith<Course>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseDto>()
                .ForMember(coursedto => coursedto.ID,
                    entity => entity.MapFrom(course => course.ID))
                .ForMember(coursedto => coursedto.Name,
                    entity => entity.MapFrom(course => course.Name));
        }
    }
}
