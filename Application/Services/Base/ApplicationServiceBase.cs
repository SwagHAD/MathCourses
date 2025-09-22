using Application.DTO.Base;
using Application.Factory.Base;
using Application.Responses;
using Domain.Entities.Base;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ApplicationServiceBase<TEntity, TDto>(IServiceProvider services) : IApplicationServiceBase<TEntity> where TEntity : BaseEntity where TDto : IDataTransferObjectBase<TEntity>
    {
        protected ICoreRepository<TEntity> CoreService {  get; } = services.GetRequiredService<ICoreRepository<TEntity>>();
        protected IValidatorFactoryBase ValidatorFactory { get; } = services.GetRequiredService<IValidatorFactoryBase>();
        protected abstract Task<Response<IDataTransferObjectBase<TEntity>>> Create(IDataTransferObjectBase<TEntity> DTO);
        protected abstract Task<Response<IDataTransferObjectBase<TEntity>>> Update(IDataTransferObjectBase<TEntity> DTO);
        protected abstract Task<Response<IDataTransferObjectBase<TEntity>>> Delete(IDataTransferObjectBase<TEntity> DTO);
    }
}
