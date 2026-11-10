using Application.Commands.DeleteCommands;
using FluentValidation;

namespace Application.Validation.Delete
{
    public sealed class PersonDeleteActionValidator : AbstractValidator<DeletePersonCommand>
    {
        public PersonDeleteActionValidator()
        {
            RuleFor(f => f.Id)
                .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
        }
    }
}
