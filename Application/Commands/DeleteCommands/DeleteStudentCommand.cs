using Application.Command.Base;
using Application.Mapping.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.DeleteCommands
{
    public sealed class DeleteStudentCommand : IBaseRequestDelete<DefaultStudentResponse>, IMapWith<Student>
    {
        public int ID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteStudentCommand, Student>()
                .ForMember(student => student.ID,
                    entity => entity.MapFrom(studentdto => studentdto.ID));
        }
    }
}
