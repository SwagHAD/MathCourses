using ProtoBuf;
using System.ComponentModel.DataAnnotations;

namespace DataMath.Entities
{
    [ProtoContract]
    public class Teacher
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }
        public List<Group> Groups { get; set; } = [];
    }
}
