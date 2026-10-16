using Application.Commands.Create;
using FluentValidation;

namespace Application.Validators.Create
{
    public sealed class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleValidator() 
        {
            RuleFor(c => c.Data.Name).NotEmpty().WithMessage("Название не может быть пустым");
            RuleFor(c => c.Data.PermissionIds).NotEmpty().WithMessage("Доступы не могут быть пустыми");
        }
    }
}
