using Application.Mapping.Base;
using Application.Responses.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.Responses
{
    public sealed class StudentWithGroupsResponse : IMapWith<Student>, IBaseResponse<Group>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DefaultGroupResponse[] Groups { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentWithGroupsResponse>()
                .ForMember(dto => dto.Id,
                    entity => entity.MapFrom(student => student.ID))
                .ForMember(dto => dto.Name,
                    entity => entity.MapFrom(student => student.Name))
                .ForMember(dto => dto.Groups,
                    entity => entity.MapFrom(student => 
                        student.StudentGroups.Select(group => new DefaultGroupResponse
                        {
                            Id = group.SecondEntityId,
                            Name = group.SecondEntity.Name,
                        })));
        }

    }
}
