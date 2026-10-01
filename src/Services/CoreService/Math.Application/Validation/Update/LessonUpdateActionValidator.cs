using Application.Commands.UpdateCommands;
using FluentValidation;

namespace Application.Validation.Update
{
    public sealed class LessonUpdateActionValidator : AbstractValidator<UpdateLessonCommand>
    {
        public LessonUpdateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}
