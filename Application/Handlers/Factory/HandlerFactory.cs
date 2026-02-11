using Application.Handlers.Base;
using Domain.Entities.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Handlers.Factory
{
    public sealed class HandlerFactory<TEntity>(IServiceProvider services) : IHandlerFactory<TEntity> where TEntity : BaseEntity
    {
        public IHandler<TEntity> GetHandle<TDto>()
        {
            return services.GetRequiredService<IHandler<TEntity>>();
        }
    }
}
