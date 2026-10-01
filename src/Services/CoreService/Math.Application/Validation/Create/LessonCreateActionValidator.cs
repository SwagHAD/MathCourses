using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class LessonCreateActionValidator : AbstractValidator<CreateLessonCommand>
    {
        public LessonCreateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}
