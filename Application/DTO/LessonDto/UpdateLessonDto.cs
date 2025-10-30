using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.LessonDTO
{
    public class UpdateLessonDto : IDataTransferObjectBaseUpdate<Lesson>, IMapWith<Lesson>
    {
        public int? ID { get; set; }
        public string Name { get; set; }

        public int? GroupID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateLessonDto, Lesson>()
                .ForMember(lesson => lesson.ID,
                    entity => entity.MapFrom(lessondto => lessondto.ID))
                .ForMember(lesson => lesson.Name,
                    entity => entity.MapFrom(lessondto => lessondto.Name))
                .ForMember(lesson => lesson.GroupID,
                    entity => entity.MapFrom(lessondto => lessondto.GroupID));
        }
    }
}
