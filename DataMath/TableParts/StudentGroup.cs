using DataMath.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.TableParts
{
    public class StudentGroup
    {
        public int Id { get; set; } // Уникальный идентификатор записи

        public int StudentId { get; set; } // Ссылка на студента (в виде идентификатора)
        public int GroupId { get; set; }   // Ссылка на группу (в виде идентификатора)

        public DateTime JoinedAt { get; set; } // Дата присоединения студента к группе
        public StatusStudent Status { get; set; }    // Дополнительное поле (например, "Активный", "Закончил")
    }

}
