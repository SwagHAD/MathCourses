using Application.Commands.Delete;
using FluentValidation;

namespace Application.Validators.Delete
{
    public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id не может быть пустым");
        }
    }
}
