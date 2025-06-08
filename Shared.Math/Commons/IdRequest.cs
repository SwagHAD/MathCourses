using ProtoBuf;

namespace Shared.Math.Commons
{
    [ProtoContract]
    public class IdRequest
    {
        [ProtoMember(1)]
        public int? ID {  get; set; }
    }
}
