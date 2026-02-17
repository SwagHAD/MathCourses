using Application.Command.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateStudentCommand : IDtoBaseUpdate<Student>, IMapWith<Student>, IRequest<Student>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateStudentCommand, Student>()
                .ForMember(studentdto => studentdto.ID,
                    entity => entity.MapFrom(student => student.ID))
                .ForMember(studentdto => studentdto.Name,
                    entity => entity.MapFrom(student => student.Name));
        }
    }
}
