using AutoMapper;
using BLL.Mapping;
using DataMath.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MR.StudentMR.Queries.Dto
{
    public class StudentDetailsDto : IMapWith<Student>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudentDetailsDto, Student>()
                .ForMember(dto => dto.Id,
                    opt => opt.MapFrom(student => student.Id))
                .ForMember(dto => dto.Name,
                    opt => opt.MapFrom(student => student.Name));
        }

    }
}
