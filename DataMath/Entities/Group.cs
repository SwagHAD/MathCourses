using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.Entities
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public int? TeacherId { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
