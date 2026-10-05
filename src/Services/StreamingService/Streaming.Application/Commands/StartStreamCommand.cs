using SharedKernel.Application.Commands;
using StreamingService.Application.Responses;

namespace StreamingService.Application.Commands
{
    public sealed record StartStreamCommand : ICommand<StartStreamResponse>
    {
        public int LessonId { get; init; }
    }
}
