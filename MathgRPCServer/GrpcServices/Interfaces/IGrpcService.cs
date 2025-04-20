using DataMath.Entities;
using DataMath.ServerVariables;
using ProtoBuf.Grpc.Configuration;

namespace MathgRPCServer.GrpcServices.Interfaces
{
    [Service]
    public interface IGrpcService<T> where T : class
    {
        [Operation]
        public Task<ServerResult<T>> Create(T common);

        [Operation]
        public Task<ServerResult<T>> Update(T common);

        [Operation]
        public Task<ServerResult<T>> Delete(int id);

        [Operation]
        public Task<ServerResult<T>> Get(int Id);

        [Operation]
        public Task<ServerResult<ICollection<T>>> GetAll();
    }
}
