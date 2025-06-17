using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public string Name { get; set; }

        [ProtoMember(3)]
        public Teacher Teacher { get; set; }

        [ProtoMember(4)]
        public int? TeacherId { get; set; }

        [ProtoMember(5)]
        public List<Student> Students { get; set; } = [];

        [ProtoMember(6)]
        public List<StudentGroup> StudentGroups { get; set; } = [];
    }
}
