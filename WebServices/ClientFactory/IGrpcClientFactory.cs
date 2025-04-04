using Grpc.Core;

namespace WebServices.GrpcClientFactory.ClientFactory
{
    public interface IGrpcClientFactory
    {
        TClient CreateClient<TClient>() where TClient : ClientBase<TClient>;
    }
}
