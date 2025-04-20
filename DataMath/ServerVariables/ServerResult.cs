using ProtoBuf;

namespace DataMath.ServerVariables
{
    [ProtoContract]
    public class ServerResult<T>
    {
        [ProtoMember(1)]
        public T Data { get; set; }

        [ProtoMember(2)]
        public bool HasError { get; set; }

        [ProtoMember(3)]
        public string Message { get; set; }

        [ProtoMember(4)]
        public List<ServerError> Errors { get; set; } = new();
    }

    [ProtoContract]
    public class ServerError
    {
        [ProtoMember(1)]
        public string Field { get; set; }

        [ProtoMember(2)]
        public string Message { get; set; }
    }
}
