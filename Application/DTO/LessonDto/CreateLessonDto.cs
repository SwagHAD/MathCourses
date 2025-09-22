using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.LessonDTO
{
    public class CreateLessonDto : IDataTransferObjectBase<Lesson>, IMapWith<Lesson>
    {
        public string Name { get; set; }

        public int? GroupID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateLessonDto, Lesson>()
                .ForMember(lessondto => lessondto.Name,
                    entity => entity.MapFrom(lessondto => lessondto.Name))
                .ForMember(lessondto => lessondto.GroupID,
                    entity => entity.MapFrom(lessondto => lessondto.GroupID));
        }
    }
}
