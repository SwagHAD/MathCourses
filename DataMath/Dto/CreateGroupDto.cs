using DataMath.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.Dto
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
