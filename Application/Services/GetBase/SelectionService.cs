using Application.DTO.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities.Base;
using Domain.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.Services.GetBase
{
    public sealed class SelectionService<T, TEntity>(IServiceProvider services) : ISelectionService<T, TEntity> where T : IDataTransferObjectBaseGet<TEntity> where TEntity : BaseEntity
    {
        private readonly IMathDbContext DbContext = services.GetRequiredService<IMathDbContext>();
        private readonly IMapper Mapper = services.GetRequiredService<IMapper>();
        public async Task<PageListResponse<T>> GetData(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            var res = new PageListResponse<T>();
            try
            {
                var data = DbContext.Set<TEntity>().Where(expression).ToArray();
                return PageListResponse<T>.Ok(Mapper.Map<T>(data), "Data retrieved successfully");
            }
            catch (Exception ex)
            {
                return PageListResponse<T>.Error(ex.Message);
            }
        }

        public async Task<Response<T>> GetItem(int Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
