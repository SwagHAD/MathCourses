using ProtoBuf;
using System.ComponentModel.DataAnnotations;

namespace Shared.Math.Commons
{
    [ProtoContract]
    public class RequestItem<T> where T : class
    {
        [Required]
        public T Item { get; set; }
    }
}
