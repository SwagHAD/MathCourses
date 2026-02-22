using Application.Commands.CreateCommands;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class LessonCreateActionValidator : BaseValidator<CreateLessonCommand>
    {
        public LessonCreateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}
