using Application.Commands.Delete;
using FluentValidation;

namespace Application.Validators.Delete
{
    public sealed class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id должен быть больше 0");
        }
    }
}
