using Application.Commands.Update;
using FluentValidation;

namespace Application.Validators.Update
{
    public sealed class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Id должен быть больше 0");
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Название не может быть пустым");
            RuleFor(c => c.PermissionIds)
                .NotEmpty()
                .WithMessage("Доступы не могут быть пустыми");
        }
    }
}
