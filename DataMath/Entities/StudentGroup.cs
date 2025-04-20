using DataMath.Enums;
using ProtoBuf;

namespace DataMath.Entities
{
    [ProtoContract]
    public class StudentGroup
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public int StudentId { get; set; }
        [ProtoMember(3)]
        public Student Student { get; set; }
        public int GroupId { get; set; }   // Ссылка на группу (в виде идентификатора)
        public Group Group { get; set; }
        public DateTime JoinedAt { get; set; } // Дата присоединения студента к группе
        public StatusStudent Status { get; set; }    // Дополнительное поле (например, "Активный", "Закончил")
    }
}