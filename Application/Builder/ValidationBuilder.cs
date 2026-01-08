using Application.DTO.Base;
using Domain.Entities.Base;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Builder
{
    public sealed class ValidationBuilder<TDto, TEntity>
        where TEntity : BaseEntity where TDto : IDTOBase
    {
        private readonly TDto _dto;
        private IValidator<TDto> _structuralValidator { get; set; }
        private Func<TDto, ValidationResult, Task> _businessValidator { get; set; }

        private ValidationBuilder(TDto dto)
            => _dto = dto;
        public static ValidationBuilder<TDto, TEntity> For(TDto dto)
            => new ValidationBuilder<TDto, TEntity>(dto);
        public ValidationBuilder<TDto, TEntity> WithStructuralValidation(IValidator<TDto> validator)
        {
            _structuralValidator = validator;
            return this;
        }

        public ValidationBuilder<TDto, TEntity> WithBusinessValidation(
            Func<TDto, ValidationResult, Task> businessValidator)
        {
            _businessValidator = businessValidator;
            return this;
        }
        public async ValueTask<ValidationResult> ValidateAsync()
        {
            var result = await _structuralValidator.ValidateAsync(_dto);
            if (!result.IsValid)
                return result;
            if(_businessValidator != null)
                await _businessValidator.Invoke(_dto, result);
            return result;
        }
    }
}
