using Application.DTO.Base;
using FluentValidation;

namespace Application.Factory.Base
{
    public interface IValidatorFactoryBase
    {
        IValidator<T> GetValidator<T>() where T : IDataTransferObjectBase;
    }
}
