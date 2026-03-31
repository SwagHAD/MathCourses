using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateLessonCommand : IRequest<DefaultLessonResponse>, IMapWith<Lesson>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? GroupID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateLessonCommand, Lesson>()
                .ForMember(lesson => lesson.ID,
                    entity => entity.MapFrom(lessondto => lessondto.ID))
                .ForMember(lesson => lesson.Name,
                    entity => entity.MapFrom(lessondto => lessondto.Name))
                .ForMember(lesson => lesson.GroupID,
                    entity => entity.MapFrom(lessondto => lessondto.GroupID));
        }
    }
}
