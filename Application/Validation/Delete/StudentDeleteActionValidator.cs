using Application.Commands.DeleteCommands;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Delete
{
    public sealed class StudentDeleteActionValidator : BaseValidator<DeleteStudentCommand>
    {
        public StudentDeleteActionValidator()
        {
            RuleFor(f => f.ID)
                .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
        }
    }
}
