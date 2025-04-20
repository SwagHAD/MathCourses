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
        [ProtoMember(4)]
        public int GroupId { get; set; }
        [ProtoMember(5)]
        public Group Group { get; set; }
        [ProtoMember(6)]
        public DateTime JoinedAt { get; set; }
        [ProtoMember(7)]
        public StatusStudent Status { get; set; }
    }
}