using Application.Command.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.Commands.CreateCommands
{
    public class CreateLessonCommand : IDtoBaseCreate<Lesson>, IMapWith<Lesson>
    {
        public string Name { get; set; }

        public int? GroupID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateLessonCommand, Lesson>()
                .ForMember(lessondto => lessondto.Name,
                    entity => entity.MapFrom(lessondto => lessondto.Name))
                .ForMember(lessondto => lessondto.GroupID,
                    entity => entity.MapFrom(lessondto => lessondto.GroupID));
        }
    }
}
