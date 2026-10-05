using MediatR;
using SharedKernel.Application.Commands;

namespace StreamingService.Application.Commands
{
    public sealed record StopStreamCommand : ICommand<Unit>
    {
        public int LessonId { get; init; }
    }
}
