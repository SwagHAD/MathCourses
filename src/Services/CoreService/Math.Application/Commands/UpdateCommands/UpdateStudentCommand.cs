using SharedKernel.Application.Commands;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UpdateCommands
{
    public sealed class UpdateStudentCommand : ICommand<Unit>, IMapWith<Student>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
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
