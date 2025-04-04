using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using WebServices.GrpcClientFactory.ClientFactory;

namespace WebServices.ClientFactory
{
    public class GrpcClientFactory : IGrpcClientFactory
    {
        private readonly GrpcChannel grpcChannel;

        public GrpcClientFactory(string serverUrl)
        {
            grpcChannel = GrpcChannel.ForAddress(serverUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            });
        }
        public TClient CreateClient<TClient>() where TClient : ClientBase<TClient>
        {
            return (TClient)Activator.CreateInstance(typeof(TClient), grpcChannel)!;
        }
    }
}
