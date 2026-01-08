using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.LessonDTO
{
    public class LessonDto : IDTOBase<Lesson>, IMapWith<Lesson>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int? GroupID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, LessonDto>()
                .ForMember(lessondto => lessondto.ID,
                    entity => entity.MapFrom(lesson => lesson.ID))
                .ForMember(lessondto => lessondto.Name,
                    entity => entity.MapFrom(lesson => lesson.Name))
                .ForMember(lessondto => lessondto.GroupID,
                    entity => entity.MapFrom(lesson => lesson.GroupID));
        }
    }
}
