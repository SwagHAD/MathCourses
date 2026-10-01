using Application.Mapping.Base;
using Application.Responses.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.Responses
{
    public sealed class DefaultStudentResponse : IMapWith<Student>, IResponse<Student>
    {
        public int Id { get; set; }
        public string Name { get; set;  }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, DefaultStudentResponse>()
                .ForMember(dto => dto.Id,
                    entity => entity.MapFrom(student => student.ID))
                .ForMember(dto => dto.Name,
                    entity => entity.MapFrom(student => student.Name));
        }
    }
}
