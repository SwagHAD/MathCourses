using Application.Mapping.Base;
using Application.Responses.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.Responses
{
    public sealed class DefaultLessonResponse : IMapWith<Lesson>, IBaseResponse<Lesson>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Lesson, DefaultLessonResponse>()
                .ForMember(dto => dto.Id,
                    entity => entity.MapFrom(group => group.ID))
                .ForMember(dto => dto.Name,
                    entity => entity.MapFrom(group => group.Name));
        }
    }
}
