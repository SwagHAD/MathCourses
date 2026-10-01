using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamingService.Application.Commands;
using StreamingService.Application.Responses;

namespace StreamingService.Controllers
{
    [ApiController]
    [Route("api/streams")]
    public sealed class StreamController(IMediator mediator) : ControllerBase
    {

        [HttpPost("{lessonId:int}/start")]
        public async Task<ActionResult<StartStreamResponse>> Start(int lessonId, CancellationToken ct)
        {
            var command = new StartStreamCommand()
            {
                LessonId = lessonId
            };
            var result = await mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPost("{lessonId:int}/stop")]
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
