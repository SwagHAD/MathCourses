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
            try
            {
                var validationResult = await ValidateItemAsync<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<TDtoBase>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                else
                {
                    var result = await UnitOfWork.ExecuteAsync(async () => 
                    {
                        var newEntity = Mapper.Map<TEntity>(dto);
                        await DbContext.AddAsync(newEntity);
                        await DbContext.SaveChangesAsync();
                        return newEntity;
                    }, IsAtomicOperation);
                    return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(result), "Успешно");
                }
            }
            catch (Exception ex)
            {
                return Response<TDtoBase>.Error(ex.Message);
            }
        }

        public async Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDataTransferObjectBaseDelete<TEntity>
        {
            try
            {
                var validationResult = await ValidateItemAsync<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<bool>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                else
                {
                    await UnitOfWork.ExecuteAsync(async () =>
                    {
                        await DbContext.Set<TEntity>().Where(f => f.ID == dto.ID)
                            .ExecuteDeleteAsync();
                        await DbContext.SaveChangesAsync();
                    });
                    return Response<bool>.Ok(true, "Успешно");
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Error(ex.Message);
            }
        }
        public async Task<Response<TDtoBase>> UpdateItemAsync<TDto>(TDto dto, bool IsAtomicOperation = true) where TDto : IDataTransferObjectBaseUpdate<TEntity>
        {
            try
            {
                var validationResult = await ValidateItemAsync<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<TDtoBase>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                else
                {
                    var result = await UnitOfWork.ExecuteAsync(async () =>
                    {
                        var updateentity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(f => f.ID == dto.ID) ?? throw new ApplicationException("Сущность не найдена");
                        updateentity.FillEntity(dto);
                        await DbContext.SaveChangesAsync();
                        return await DbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(f => f.ID == dto.ID) ?? throw new ApplicationException("Сущность не найдена");
                    });
                    return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(result), "Успешно");
                }
            }
            catch (Exception ex)
            {
                return Response<TDtoBase>.Error(ex.Message);
            }
        }
    }
}
