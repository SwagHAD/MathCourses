using Application.Mapping.Base;
using Application.Responses.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.Responses
{
    public sealed class GroupResponse : IMapWith<Group>, IResponse<Group>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DefaultStudentResponse[] Students { get; set; }

        public DefaultTeacherResponse[] Teachers { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupResponse>()
                .ForMember(dto => dto.Id,
                    entity => entity.MapFrom(student => student.ID))
                .ForMember(dto => dto.Name,
                    entity => entity.MapFrom(student => student.Name))
                .ForMember(dto => dto.Students,
                    entity => entity.MapFrom(student =>
                        student.StudentGroups.Select(group => new DefaultStudentResponse
                        {
                            Id = group.FirstEntityId,
                            Name = group.FirstEntity.Name
                        }).ToArray()))
                .ForMember(dto => dto.Teachers,
                    entity => entity.MapFrom(group => 
                        group.TeacherGroups.Select(teacher => new DefaultTeacherResponse
                        {
                            Id = teacher.FirstEntityId,
                            Name = teacher.FirstEntity.Name
                        }).ToArray()));
        }
    }
}
