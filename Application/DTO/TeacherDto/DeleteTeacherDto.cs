using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.TeacherDTO
{
    public class DeleteTeacherDto : IDataTransferObjectBase<Teacher> , IMapWith<Teacher>
    {
        public int Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTeacherDto, Teacher>()
                .ForMember(teacher => teacher.ID,
                    entity => entity.MapFrom(teacherdto => teacherdto.Id));
        }
    }
}
