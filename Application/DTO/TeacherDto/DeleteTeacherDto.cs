using Application.DTO.Base;
using Application.Mapping.Base;
using AutoMapper;
using Domain.Entities;

namespace Application.DTO.TeacherDTO
{
    public class DeleteTeacherDto : IDataTransferObjectBaseDelete<Teacher> , IMapWith<Teacher>
    {
        public int? ID { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTeacherDto, Teacher>()
                .ForMember(teacher => teacher.ID,
                    entity => entity.MapFrom(teacherdto => teacherdto.ID));
        }
    }
}
