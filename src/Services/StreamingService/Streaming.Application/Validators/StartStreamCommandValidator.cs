using FluentValidation;
using StreamingService.Application.Commands;

namespace StreamingService.Application.Validators
{
    internal sealed class StartStreamCommandValidator : AbstractValidator<StartStreamCommand>
    {
        public StartStreamCommandValidator()
        {
            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("Lesson ID is not valid.");
        }
    }
}
