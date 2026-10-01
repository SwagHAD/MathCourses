using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            return await next();
        }
    }
}

