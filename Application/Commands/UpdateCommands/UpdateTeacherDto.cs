using Application.DTO.Base;
using Application.Mapping.Base;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public class UpdateTeacherDto : IDtoBaseUpdate<Teacher>, IMapWith<Teacher>, IRequest<Teacher>
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<int> Groups { get; set; } = [];
        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(teacher => teacher.ID,
                    entity => entity.MapFrom(teacherdto => teacherdto.ID))
                .ForMember(teacher => teacher.Name,
                    entity => entity.MapFrom(teacherdto => teacherdto.Name));
        }
    }
}
