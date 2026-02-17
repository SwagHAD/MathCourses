using Application.Command.Base;
using FluentValidation;

namespace Application.Validation.Base
{
    public class BaseValidator<T> : AbstractValidator<T> where T : IDtoBase;
}
