using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace SharedKernel.Application.Behaviors
{
    /// <summary>Прогоняет запрос через все зарегистрированные для него валидаторы. Нет валидаторов — пропускает.</summary>
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly List<ValidationFailure> _failures = new();
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            foreach (var validator in validators)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    _failures.AddRange(validationResult.Errors);
            }
            if (_failures.Count > 0)
                throw new ValidationException(_failures);
            return await next(cancellationToken);
        }
    }
}
