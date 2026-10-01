using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class StudentCreateActionValidator : AbstractValidator<CreateStudentCommand>
    {
        public StudentCreateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Имя слишком длинное");
        }
    }
}
