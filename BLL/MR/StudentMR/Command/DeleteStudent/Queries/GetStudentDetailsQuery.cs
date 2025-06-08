using BLL.MR.StudentMR.Queries.Dto;
using DataMath.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MR.StudentMR.Queries
{
    public class GetStudentDetailsQuery : IRequest<Student>
    {
        public int Id { get; set; }
    }
}
