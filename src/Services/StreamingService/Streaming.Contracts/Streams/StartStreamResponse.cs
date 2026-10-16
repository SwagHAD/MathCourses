namespace StreamingService.Contracts.Streams
{
    /// <summary>Куда публиковать (WHIP) и откуда смотреть (HLS) стрим урока.</summary>
    public sealed record StartStreamResponse
    {
        public string WhipPublishUrl { get; init; } = null!;
        public string HlsPlaybackUrl { get; init; } = null!;
        public string StreamToken { get; init; } = null!;
    }
}
