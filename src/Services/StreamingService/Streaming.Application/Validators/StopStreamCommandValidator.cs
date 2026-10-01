using FluentValidation;
using StreamingService.Application.Commands;

namespace StreamingService.Application.Validators
{
    internal sealed class StopStreamCommandValidator : AbstractValidator<StopStreamCommand>
    {
        public StopStreamCommandValidator()
        {
            RuleFor(x => x.LessonId)
                .NotEmpty().WithMessage("StreamId is required.");
        }
    }
}
