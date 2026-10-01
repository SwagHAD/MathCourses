using Application.Commands.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteCourseCommand : ICommand<Unit> , IMapWith<Course>
    {
        public int ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCourseCommand, Course>()
                .ForMember(course => course.ID,
                    entity => entity.MapFrom(coursedto => coursedto.ID));
        }
    }
}
