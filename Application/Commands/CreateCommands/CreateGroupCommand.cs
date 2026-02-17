using Application.Command.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public sealed class CreateGroupCommand : IDtoBaseCreate<Group> , IMapWith<Group>
    {
        public string Name { get; set; }
        public int? CourseID { get; set; }
        public List<int> StudentIDs { get; set; } = [];
        public List<int> TeacherIDs { get; set; } = [];

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGroupCommand, Group>()
                .ForMember(group => group.Name,
                    entity => entity.MapFrom(groupdto => groupdto.Name))
                .ForMember(group => group.CourseID,
                    entity => entity.MapFrom(groupdto => groupdto.CourseID))
                .ForMember(group => group.TeacherGroups,
                    opt => opt.MapFrom(dto =>
                        dto.TeacherIDs.Select(id => new TeacherGroup
                        {
                            FirstEntityId = id,
                        })))
                .ForMember(g => g.StudentGroups,
                    opt => opt.MapFrom(dto =>
                        dto.StudentIDs.Select(id => new StudentGroup
                        {
                            FirstEntityId = id,
                        })));
        }
    }
}
