using Application.DTO.Base;
using Application.Factory.Base;
using Application.Responses;
using Domain.Entities.Base;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public abstract class ApplicationServiceBase<TEntity>(IServiceProvider services) : IApplicationServiceBase<TEntity> where TEntity : BaseEntity
    {
        protected ICoreRepository<TEntity> CoreService {  get; } = services.GetRequiredService<ICoreRepository<TEntity>>();
        protected IValidatorFactoryBase ValidatorFactory { get; } = services.GetRequiredService<IValidatorFactoryBase>();

        public async Task<Response<IDataTransferObjectBase<TEntity>>> Create(IDataTransferObjectBase<TEntity> DTO)
        {
            try
            {
                var createValidator = ValidatorFactory.GetValidator<IDataTransferObjectBase<TEntity>>();
                var result = await createValidator.ValidateAsync(DTO);
                if(!result.IsValid)
                    return Response<IDataTransferObjectBase<TEntity>>.Fail(result.Errors.Select(e => e.ErrorMessage).ToList());
                
            }
            catch (Exception ex)
            {

            }
        }

        public Task<Response<IDataTransferObjectBase<TEntity>>> Delete(IDataTransferObjectBase<TEntity> DTO)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IDataTransferObjectBase<TEntity>>> Update(IDataTransferObjectBase<TEntity> DTO)
        {
            throw new NotImplementedException();
        }

        public
    }
}
