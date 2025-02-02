using DataMath.Enums;

namespace DataMath.Entities
{
    public class StudentGroup
    {
        public int Id { get; set; } // Уникальный идентификатор записи
        public int StudentId { get; set; } // Ссылка на студента (в виде идентификатора)
        public Student Student { get; set; }
        public int GroupId { get; set; }   // Ссылка на группу (в виде идентификатора)
        public Group Group { get; set; }
        public DateTime JoinedAt { get; set; } // Дата присоединения студента к группе
        public StatusStudent Status { get; set; }    // Дополнительное поле (например, "Активный", "Закончил")
    }
}