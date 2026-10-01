using Application.Commands.DeleteCommands;
using FluentValidation;

namespace Application.Validation.Delete
{
    public sealed class StudentDeleteActionValidator : AbstractValidator<DeleteStudentCommand>
    {
        public StudentDeleteActionValidator()
        {
            RuleFor(f => f.ID)
                .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
        }
    }
}
