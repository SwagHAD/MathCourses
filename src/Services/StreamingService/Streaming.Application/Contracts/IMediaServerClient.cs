namespace StreamingService.Application.Contracts
{
    public interface IMediaServerClient
    {
        Task CreatePathAsync(string path, CancellationToken ct);
        Task DeletePathAsync(string path, CancellationToken ct);
        string GetWhipPublishUrl(string path); 
        string GetHlsPlaybackUrl(string path);
    }
}
