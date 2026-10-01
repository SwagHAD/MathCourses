using Microsoft.Extensions.Options;
using StreamingService.Application.Contracts;
using StreamingService.Infrastructure.Options;
using System.Net.Http.Json;

namespace StreamingService.Infrastructure.MediaMtx
{
    internal sealed class MediaMtxClient(HttpClient httpClient, IOptions<MediaMtxOptions> options)
        : IMediaServerClient
    {
        private readonly string _publicBaseUrl = options.Value.PublicUrl.TrimEnd('/');

        public async Task CreatePathAsync(string path, CancellationToken ct)
        {
            var payload = new
            {
                source = "publisher",
                record = true,
                recordPath = $"/recordings/{path}/%Y-%m-%d_%H-%M-%S-%f",
                recordFormat = "fmp4"
            };

            var response = await httpClient.PostAsJsonAsync($"/v3/config/paths/add/{path}", payload, ct);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePathAsync(string path, CancellationToken ct)
        {
            var response = await httpClient.DeleteAsync($"/v3/config/paths/delete/{path}", ct);
            response.EnsureSuccessStatusCode();
        }

        public string GetWhipPublishUrl(string path) => $"{_publicBaseUrl}/{path}/whip";
        public string GetHlsPlaybackUrl(string path) => $"{_publicBaseUrl}/{path}/index.m3u8";
    }
}