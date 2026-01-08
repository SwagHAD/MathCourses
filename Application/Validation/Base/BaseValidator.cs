using Application.DTO.Base;
using FluentValidation;

namespace Application.Validation.Base
{
    public class BaseValidator<T> : AbstractValidator<T> where T : IDTOBase;
}
