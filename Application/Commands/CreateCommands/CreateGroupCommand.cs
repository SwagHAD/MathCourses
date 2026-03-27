using Application.Command.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.CreateCommands
{
    public sealed class CreateGroupCommand : IBaseRequestCreate<Group> , IMapWith<Group>
    {
        public string Name { get; set; }
        public int? CourseID { get; set; }
        public List<int> Students { get; set; } = [];
        public List<int> Teachers { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGroupCommand, Group>()
                .ForMember(group => group.Name,
                    entity => entity.MapFrom(groupdto => groupdto.Name))
                .ForMember(group => group.CourseID,
                    entity => entity.MapFrom(groupdto => groupdto.CourseID))
                .ForMember(group => group.TeacherGroups,
                    opt => opt.MapFrom(dto =>
                        dto.Teachers.Select(id => new TeacherGroup
                        {
                            FirstEntityId = id,
                        }).ToArray()))
                .ForMember(g => g.StudentGroups,
                    opt => opt.MapFrom(dto =>
                        dto.Students.Select(id => new StudentGroup
                        {
                            FirstEntityId = id,
                        }).ToArray()));
        }
    }
}
