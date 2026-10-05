using MassTransit;
using MediatR;
using StreamingService.Application.Commands;
using StreamingService.Application.Contracts;
using StreamingService.Application.Responses;
using StreamingService.Domain.Entities;
using StreamingService.Domain.Enums;
using SharedKernel.Interfaces;
using SharedKernel.Application.Interfaces;

namespace StreamingService.Application.Handlers
{
    public sealed class StartStreamHandler(IMediaServerClient mediaServerClient, 
        ISwagDbContext streamDbContext, IUserProvider userProvider) : IRequestHandler<StartStreamCommand, StartStreamResponse>
    {
        public async Task<StartStreamResponse> Handle(StartStreamCommand request, CancellationToken cancellationToken)
        {
            var session = new StreamSession()
            {
                LessonId = request.LessonId,
                UserId = userProvider.GetUserId(),
                StreamPath = $"lesson-{request.LessonId:N}",
                StreamToken = Guid.NewGuid().ToString("N"),
                StreamStatus = StreamStatus.Live,
                StartedAt = DateTime.UtcNow
            };

            await mediaServerClient.CreatePathAsync(session.StreamPath, cancellationToken);
            await streamDbContext.Set<StreamSession>().AddAsync(session, cancellationToken);

            //await publishEndpoint.Publish(new StreamStartedEvent(session.LessonId, mediaServerClient.GetHlsPlaybackUrl(session.StreamPath)), cancellationToken);
            await streamDbContext.SaveChangesAsync(cancellationToken);
            return new StartStreamResponse(
                whipPublishUrl: mediaServerClient.GetWhipPublishUrl(session.StreamPath),
                hlsPlaybackUrl: mediaServerClient.GetHlsPlaybackUrl(session.StreamPath),
                streamToken: session.StreamToken
            );
        }
    }
}
