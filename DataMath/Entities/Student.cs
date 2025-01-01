using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.Entities
{
    public class Student
    {
        public int Id { get; set; } // Уникальный идентификатор студента
        public string Name { get; set; } // Имя студента
        public List<Group> Groups { get; set; } = new();
    }
}
