using DataMath.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MR.StudentMR.Command.CreateStudent
{
    public class CreateStudentCommand : IRequest<Student>
    {
        public string Name { get; set; }
    }
}
