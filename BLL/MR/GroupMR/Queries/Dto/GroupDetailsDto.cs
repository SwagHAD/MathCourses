using AutoMapper;
using BLL.Mapping;
using DataMath.Entities;

namespace Core.MR.GroupMR.Queries.Dto
{
    public class GroupDetailsDto : IMapWith<Group>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students = new List<Student>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupDetailsDto>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(group => group.Id))
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(group => group.Name))
                .ForMember(dto => dto.Teacher.Id,
                    opt => opt.MapFrom(group => group.TeacherId))
                .ForMember(dto => dto.Students,
                    opt => opt.MapFrom(group => group.Students));
        }
    }
}
