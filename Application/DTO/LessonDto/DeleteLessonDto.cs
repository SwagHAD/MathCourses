using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.LessonDTO
{
    public class DeleteLessonDto : IDataTransferObjectBase<Lesson>, IMapWith<Lesson>
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteLessonDto, Lesson>()
                .ForMember(lesson => lesson.ID,
                    entity => entity.MapFrom(lessondto => lessondto.Id));
        }
    }
}
