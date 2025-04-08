using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.Entities
{
    public class Student
    {
        public int Id { get; set; } // Уникальный идентификатор студента
        [MaxLength(50)]
        public string Name { get; set; } // Имя студента

        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
