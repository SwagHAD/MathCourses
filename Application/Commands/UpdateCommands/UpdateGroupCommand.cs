using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateGroupCommand : IRequest<GroupResponse>, IMapWith<Group>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<int> Teachers { get; set; } = [];

        public int? CourseID { get; set; }

        public List<int> Students { get; set; } = [];
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGroupCommand, Group>()
                .ForMember(group => group.Name,
                    entity => entity.MapFrom(groupdto => groupdto.Name))
                .ForMember(group => group.CourseID,
                    entity => entity.MapFrom(groupdto => groupdto.CourseID))
                .ForMember(group => group.TeacherGroups,
                    opt => opt.MapFrom(dto =>
                        dto.Teachers.Select(id => new TeacherGroup
                        {
                            FirstEntityId = id,
                        })))
                .ForMember(g => g.StudentGroups,
                    opt => opt.MapFrom(dto =>
                        dto.Students.Select(id => new StudentGroup
                        {
                            FirstEntityId = id,
                        })));
        }
    }
}
