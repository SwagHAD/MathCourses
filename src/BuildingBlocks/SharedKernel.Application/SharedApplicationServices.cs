using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Behaviors;
using System.Reflection;

namespace SharedKernel.Application
{
    public static class SharedApplicationServices
    {
        /// <summary>
        /// Регистрирует MediatR-обработчики и валидаторы из сборки Application-слоя сервиса
        /// и общие behaviors: сначала валидация, затем behaviors сервиса, транзакция — последней.
        /// </summary>
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, Assembly applicationAssembly,
            Action<MediatRServiceConfiguration>? configureBehaviors = null)
        {
            services.AddValidatorsFromAssembly(applicationAssembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(applicationAssembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                configureBehaviors?.Invoke(cfg);
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });
            return services;
        }
    }
}
