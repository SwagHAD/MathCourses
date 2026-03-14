using AutoMapper;
using Domain.Entities.Base;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public partial class CrudServiceBase<TEntity> : ICrudServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        protected IUnitOfWork<TEntity> UnitOfWork { get; } = services.GetRequiredService<IUnitOfWork<TEntity>>();
        protected IMapper Mapper { get; } = services.GetRequiredService<IMapper>();
        protected IMediator Mediator = services.GetRequiredService<IMediator>();
    }
}
