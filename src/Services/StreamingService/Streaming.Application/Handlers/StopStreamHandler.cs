using MediatR;
using Microsoft.EntityFrameworkCore;
using StreamingService.Application.Commands;
using StreamingService.Application.Contracts;
using StreamingService.Domain.Enums;
using SharedKernel.Application.Interfaces;
using StreamingService.Domain.Entities;

namespace StreamingService.Application.Handlers
{
    internal sealed class StopStreamHandler(ISwagDbContext streamDbContext, IMediaServerClient mediaServerClient) : IRequestHandler<StopStreamCommand, Unit>
    {
        public async Task<Unit> Handle(StopStreamCommand request, CancellationToken cancellationToken)
        {
            var session = await streamDbContext.Set<StreamSession>().FirstOrDefaultAsync(f => f.LessonId == request.LessonId && f.StreamStatus == StreamStatus.Live, cancellationToken)
                ?? throw new Exception($"Active stream for lesson {request.LessonId} not found");

            var recordingPath = $"/recordings/{session.StreamPath}";

            await mediaServerClient.DeletePathAsync(session.StreamPath, cancellationToken);
            session.StreamStatus = StreamStatus.Ended;
            session.RecordingPath = recordingPath;
            session.EndedAt = DateTime.UtcNow;
            await streamDbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
