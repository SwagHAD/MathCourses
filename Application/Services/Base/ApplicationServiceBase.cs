using Application.DTO.Base;
using Application.Extensions;
using Application.Responses;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Base
{
    /// <summary>
    /// Базовый класс по создание бизнес кейсов
    /// Классы наследники помечать как sealed
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы</typeparam>
    /// <typeparam name="TDtoBase">Базовая ДТО</typeparam>
    /// <param name="services">Базовый сервис</param>
    public partial class ApplicationServiceBase<TEntity, TDtoBase>(IServiceProvider services) : IApplicationServiceBase<TEntity, TDtoBase>
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        public async Task<Response<TDtoBase>> CreateItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDataTransferObjectBaseCreate<TEntity>
        {
            return await Handle<TDto, TDtoBase>(async() =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    await CustomCreate(dto);
                    var newEntity = Mapper.Map<TEntity>(dto);
                    await DbContext.AddAsync(newEntity);
                    await DbContext.SaveChangesAsync();
                    return newEntity;
                });
            }, dto);
        }

        public async Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDataTransferObjectBaseDelete<TEntity>
        {
            return await Handle<TDto>(async () =>
                await UnitOfWork.ExecuteAsync(async () =>
                    {
                        await CustomDelete(dto);
                        await DbContext.Set<TEntity>().Where(f => f.ID == dto.ID)
                            .ExecuteDeleteAsync();
                        await DbContext.SaveChangesAsync();
                    })
                ,dto);
        }
        public async Task<Response<TDtoBase>> UpdateItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDataTransferObjectBaseUpdate<TEntity>
        {
            return await Handle<TDto, TDtoBase>(async () =>
            {
                return await UnitOfWork.ExecuteAsync(async () =>
                {
                    await CustomUpdate(dto);
                    var updateentity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(f => f.ID == dto.ID) ?? throw new ApplicationException("Сущность не найдена");
                    updateentity.FillEntity(dto);
                    await DbContext.SaveChangesAsync();
                    return updateentity;
                });
            },dto);
        }

        
    }
}
