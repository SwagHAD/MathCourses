using Application.Command.Base;
using FluentValidation;

namespace Application.Factory.Base
{
    public interface IValidatorFactoryBase
    {
        IValidator<T> GetValidator<T>() where T : IDtoBase;
    }
}
