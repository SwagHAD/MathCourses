using Application.Commands.Update;
using FluentValidation;

namespace Application.Validators.Update
{
    public sealed class UpdateRoleValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleValidator()
        {
            RuleFor(c => c.Data.Id)
                .GreaterThan(0)
                .WithMessage("Id должен быть больше 0");
            RuleFor(c => c.Data.Name)
                .NotEmpty()
                .WithMessage("Название не может быть пустым");
            RuleFor(c => c.Data.Permissions)
                .NotEmpty()
                .WithMessage("Доступы не могут быть пустыми");
            RuleFor(c => c.Data.Permissions)
                .Must(grants => grants.Select(g => g.PermissionId).Distinct().Count() == grants.Count)
                .WithMessage("Одно право нельзя выдать роли дважды");
            RuleForEach(c => c.Data.Permissions).ChildRules(grant =>
                grant.RuleFor(g => g.Scope).IsInEnum().WithMessage("Неизвестная область действия права"));
        }
    }
}
