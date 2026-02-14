using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.CreateCommands
{
    public class CreateLessonDto : IDtoBaseCreate<Lesson>, IMapWith<Lesson>, IRequest<Lesson>
    {
        public string Name { get; set; }

        public int? GroupID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateLessonDto, Lesson>()
                .ForMember(lessondto => lessondto.Name,
                    entity => entity.MapFrom(lessondto => lessondto.Name))
                .ForMember(lessondto => lessondto.GroupID,
                    entity => entity.MapFrom(lessondto => lessondto.GroupID));
        }
    }
}
