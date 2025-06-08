using DataMath.ServerVariables;
using ProtoBuf.Grpc.Configuration;
using Shared.Math.Commons;

namespace MathgRPCServer.GrpcServices.Interfaces
{
    [Service]
    public interface IGrpcService<T> where T : class
    {
        [Operation]
        public Task<ServerResult<T>> Create(RequestItem<T> common);

        [Operation]
        public Task<ServerResult<T>> Update(RequestItem<T> common);

        [Operation]
        public Task<ServerResult<T>> Delete(IdRequest ID);

        [Operation]
        public Task<ServerResult<T>> Get(IdRequest ID);

        [Operation]
        public Task<ServerResult<ICollection<T>>> GetAll(IdRequest ID);
    }
}
