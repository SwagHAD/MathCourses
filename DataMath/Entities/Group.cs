using ProtoBuf;
using System.ComponentModel.DataAnnotations;

namespace DataMath.Entities
{
    [ProtoContract]
    public class Group
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [MaxLength(10)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public Teacher Teacher { get; set; }

        [ProtoMember(4)]
        public int? TeacherId { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
