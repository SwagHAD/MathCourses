using Application.Mapping.Base;
using Application.Responses.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.Responses
{
    public sealed class DefaultTeacherResponse : IMapWith<Teacher>, IBaseResponse<Teacher>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, DefaultTeacherResponse>()
                .ForMember(dto => dto.Id,
                    entity => entity.MapFrom(teacher => teacher.ID))
                .ForMember(dto => dto.Name,
                    entity => entity.MapFrom(teacher => teacher.Name));
        }
    }
}
