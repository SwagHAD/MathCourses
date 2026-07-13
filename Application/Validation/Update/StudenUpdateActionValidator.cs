using Application.Commands.UpdateCommands;
using FluentValidation;

namespace Application.Validation.Update
{
    public sealed class StudenUpdateActionValidator : AbstractValidator<UpdateStudentCommand>
    {
        public StudenUpdateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}
