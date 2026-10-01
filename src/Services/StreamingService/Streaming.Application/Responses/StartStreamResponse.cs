namespace StreamingService.Application.Responses
{
    public sealed record StartStreamResponse
    {
        public string WhipPublishUrl { get; init; }
        public string HlsPlaybackUrl { get; init; }
        public string StreamToken { get; init; }

        public StartStreamResponse(string whipPublishUrl, string hlsPlaybackUrl, string streamToken)
        {
            WhipPublishUrl = whipPublishUrl;
            HlsPlaybackUrl = hlsPlaybackUrl;
            StreamToken = streamToken;
        }
    }
}
