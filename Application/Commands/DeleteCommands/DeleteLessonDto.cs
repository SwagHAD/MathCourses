using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public class DeleteLessonDto : IDtoBaseDelete<Lesson>, IMapWith<Lesson>, IRequest<Lesson>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteLessonDto, Lesson>()
                .ForMember(lesson => lesson.ID,
                    entity => entity.MapFrom(lessondto => lessondto.ID));
        }
    }
}
