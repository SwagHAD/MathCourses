using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class TeacherCreateActionValidator : AbstractValidator<CreateTeacherCommand>
    {
        public TeacherCreateActionValidator() 
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Имя слишком длинное");
        }
    }
}
