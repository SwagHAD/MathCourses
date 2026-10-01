using Application.Commands.Create;
using FluentValidation;

namespace Application.Validators.Create
{
    public sealed class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Название не может быть пустым");
            RuleFor(c => c.PermissionIds).NotEmpty().WithMessage("Доступы не могут быть пустыми");
        }
    }
}
