using Application.Command.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteLessonCommand : IBaseRequestDelete<DefaultGroupResponse>, IMapWith<Lesson>, IRequest<Lesson>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteLessonCommand, Lesson>()
                .ForMember(lesson => lesson.ID,
                    entity => entity.MapFrom(lessondto => lessondto.ID));
        }
    }
}
