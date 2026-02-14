using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public class CreateTeacherDto : IDtoBaseCreate<Teacher>, IMapWith<Teacher>, IRequest<Teacher>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTeacherDto, Teacher>()
                .ForMember(teacher => teacher.Name,
                    entity => entity.MapFrom(teacherdto => teacherdto.Name));
        }
    }
}
