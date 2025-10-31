using Application.Builder;
using Application.DTO.Base;
using Application.Factory.Base;
using Application.Responses;
using Application.Services.UnitOfWork;
using AutoMapper;
using Domain.Entities.Base;
using Domain.Interfaces.Data;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    /// <summary>
    /// Базовый класс по создание бизнес кейсов
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы</typeparam>
    /// <typeparam name="TDtoBase">Базовая ДТО</typeparam>
    /// <param name="services">Базовый сервис</param>
    public class ApplicationServiceBase<TEntity, TDtoBase>(IServiceProvider services) : IApplicationServiceBase<TEntity, TDtoBase> 
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        protected IMathDbContext DbContext { get; } = services.GetRequiredService<IMathDbContext>();
        private IUnitOfWork<TEntity, TDtoBase> UnitOfWork { get; } = services.GetRequiredService<IUnitOfWork<TEntity, TDtoBase>>();
        protected IValidatorFactoryBase ValidatorFactory { get; } = services.GetRequiredService<IValidatorFactoryBase>();
        protected IMapper Mapper { get; } = services.GetRequiredService<IMapper>();
        protected virtual Task CustomValidate<TDto>(TDto dto, ValidationResult args) where TDto : IDataTransferObjectBase
            => Task.CompletedTask;
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
                    return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(Mapper.Map<TDtoBase>(result)), "Успешно");
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
                        var updateEntity = Mapper.Map<TEntity>(dto);
                        DbContext.Set<TEntity>().Update(updateEntity);
                        await DbContext.SaveChangesAsync();
                        return updateEntity;
                    });
                    return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(result), "Успешно");
                }
            }
            catch (Exception ex)
            {
                return Response<TDtoBase>.Error(ex.Message);
            }
        }

        protected virtual async ValueTask<ValidationResult> ValidateItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBase<TEntity>
        {
            return await ValidationBuilder<TDto, TEntity>
                .For(dto)
                .WithStructuralValidation(ValidatorFactory.GetValidator<TDto>())
                .WithBusinessValidation(CustomValidate)
                .ValidateAsync();
        }
    }
}
