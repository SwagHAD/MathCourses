using Application.Command.Base;
using FluentValidation;
using MediatR;

namespace Application.Validation.Base
{
    public class BaseValidator<T> : AbstractValidator<T> where T : IBaseRequest;
}
