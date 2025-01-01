using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Group> Groups { get; set; } = new();
    }
}
