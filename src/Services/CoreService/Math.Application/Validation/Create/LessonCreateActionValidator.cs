using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class LessonCreateActionValidator : AbstractValidator<CreateLessonCommand>
    {
        public LessonCreateActionValidator()
        {
            RuleFor(x => x.Data.CourseId)
                .GreaterThan(0).WithMessage("Курс обязателен");
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
