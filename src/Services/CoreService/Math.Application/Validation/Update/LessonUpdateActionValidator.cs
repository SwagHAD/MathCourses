using Application.Commands.UpdateCommands;
using FluentValidation;

namespace Application.Validation.Update
{
    public sealed class LessonUpdateActionValidator : AbstractValidator<UpdateLessonCommand>
    {
        public LessonUpdateActionValidator()
        {
            RuleFor(x => x.Data.Id)
                .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(256).WithMessage("Название слишком длинное");
            RuleFor(x => x.Data.Description)
                .MaximumLength(4000).WithMessage("Описание слишком длинное");
            RuleFor(x => x.Data.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Номер урока не может быть отрицательным");
        }
    }
}
