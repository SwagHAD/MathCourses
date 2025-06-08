using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace DataMath.Entities
{
    [ProtoContract]
    public class Student
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public ICollection<Group> Groups { get; set; }
    }
}