using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public class DeleteTeacherDto : IDtoBaseDelete<Teacher> , IMapWith<Teacher>, IRequest<Teacher>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTeacherDto, Teacher>()
                .ForMember(teacher => teacher.ID,
                    entity => entity.MapFrom(teacherdto => teacherdto.ID));
        }
    }
}
