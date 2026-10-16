using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamingService.Application.Commands;
using StreamingService.Contracts;
using StreamingService.Contracts.Streams;

namespace StreamingService.Controllers
{
    [ApiController]
    [Route(StreamingRoutes.Prefix)]
    public sealed class StreamController(IMediator mediator) : ControllerBase
    {

        [HttpPost(StreamingRoutes.Start)]
        public async Task<ActionResult<StartStreamResponse>> Start(int lessonId, CancellationToken ct)
        {
            var command = new StartStreamCommand()
            {
                LessonId = lessonId
            };
            var result = await mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPost(StreamingRoutes.Stop)]
        public async Task<IActionResult> Stop(int lessonId, CancellationToken ct)
        {
            var command = new StopStreamCommand()
            {
                LessonId = lessonId
            };
            await mediator.Send(command, ct);
            return NoContent();
        }
    }
}
