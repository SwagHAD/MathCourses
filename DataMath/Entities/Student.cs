using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProtoBuf;

namespace DataMath.Entities
{
    [ProtoContract]
    public class Student
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public List<Group> Groups { get; set; }
        [ProtoMember(4)]
        public List<StudentGroup> StudentGroups { get; set; } = [];
    }
}