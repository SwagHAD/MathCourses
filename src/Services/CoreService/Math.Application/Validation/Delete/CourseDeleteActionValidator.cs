using Application.Commands.DeleteCommands;
using FluentValidation;

namespace Application.Validation.Delete
{
    public sealed class CourseDeleteActionValidator : AbstractValidator<DeleteCourseCommand>
    {
        public CourseDeleteActionValidator() 
        {
            RuleFor(f => f.ID)
                .GreaterThan(0)
                .WithMessage("Индификатор должен быть больше 0");
        }
    }
}
