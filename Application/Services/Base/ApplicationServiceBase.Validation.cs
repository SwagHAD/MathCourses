using Application.Builder;
using Application.DTO.Base;
using Domain.Entities.Base;
using FluentValidation.Results;

namespace Application.Services.Base
{
    public partial class ApplicationServiceBase<TEntity, TDtoBase> : IApplicationServiceBase<TEntity, TDtoBase>
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        /// <summary>
        /// Расширение логики валидации
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual Task CustomValidate<TDto>(TDto dto, ValidationResult args) where TDto : IDataTransferObjectBase
            => Task.CompletedTask;

        protected virtual async ValueTask<ValidationResult> ValidateItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBase<TEntity>
        {
            return await ValidationBuilder<TDto, TEntity>
                .For(dto)
                .WithStructuralValidation(ValidatorFactory.GetValidator<TDto>())
                .WithBusinessValidation(CustomValidate)
                .ValidateAsync();
        }
    }
}
