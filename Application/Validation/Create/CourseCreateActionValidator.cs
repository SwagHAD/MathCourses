using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class CourseCreateActionValidator : AbstractValidator<CreateCourseCommand>
    {
        public CourseCreateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}
