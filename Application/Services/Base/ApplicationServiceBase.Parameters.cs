using Application.DTO.Base;
using Application.Factory.Base;
using Application.Services.UnitOfWork;
using AutoMapper;
using Domain.Entities.Base;
using Domain.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    public partial class ApplicationServiceBase<TEntity, TDtoBase> : IApplicationServiceBase<TEntity, TDtoBase>
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        protected IMathDbContext DbContext { get; } = services.GetRequiredService<IMathDbContext>();
        protected IUnitOfWork<TEntity, TDtoBase> UnitOfWork { get; } = services.GetRequiredService<IUnitOfWork<TEntity, TDtoBase>>();
        protected IValidatorFactoryBase ValidatorFactory { get; } = services.GetRequiredService<IValidatorFactoryBase>();
        protected IMapper Mapper { get; } = services.GetRequiredService<IMapper>();
    }
}
