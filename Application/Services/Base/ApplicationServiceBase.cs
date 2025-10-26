using Application.DTO.Base;
using Application.Factory.Base;
using Application.Responses;
using AutoMapper;
using Domain.Entities.Base;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Base
{
    /// <summary>
    /// Базовый класс по создание бизнес кейсов
    /// </summary>
    /// <typeparam name="TEntity">Сущность базы</typeparam>
    /// <typeparam name="TDtoBase">Базовая ДТО</typeparam>
    /// <param name="services">Базовый сервис</param>
    public abstract class ApplicationServiceBase<TEntity, TDtoBase>(IServiceProvider services) : IApplicationServiceBase<TEntity, TDtoBase> 
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        protected IMathDbContext DbContext { get; } = services.GetRequiredService<IMathDbContext>();
        protected IValidatorFactoryBase ValidatorFactory { get; } = services.GetRequiredService<IValidatorFactoryBase>();
        protected IMapper Mapper { get; } = services.GetRequiredService<IMapper>();

        protected virtual Task BeforeAction<TDto>(TDto dto, ValidationResult args) where TDto : IDataTransferObjectBase<TEntity>
            => Task.CompletedTask;
        protected virtual Task AfterAction()
            => Task.CompletedTask;
        protected virtual Task Validate()
            => Task.CompletedTask;
        public async Task<Response<TDtoBase>> CreateItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBaseCreate<TEntity>
        {
            try
            {
                var validationResult = await ValidateItem<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<TDtoBase>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                var entity = await CoreService.CreateItemAsync(Mapper.Map<TEntity>(dto));
                return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(entity), "Успешно");
            }
            catch (Exception ex)
            {
                return Response<TDtoBase>.Error(ex.Message);
            }
        }

        public async Task<Response<bool>> DeleteItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBaseDelete<TEntity>
        {
            try
            {
                var validationResult = await ValidateItem<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<bool>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                await CoreService.DeleteItemAsync(Mapper.Map<TEntity>(dto).ID);
                return Response<bool>.Ok(true , "Успешно");
            }
            catch (Exception ex)
            {
                return Response<bool>.Error(ex.Message);
            }
        }

        public async Task<Response<TDtoBase>> GetItemAsync(int? ID)
        {
            try
            {
                var entity = await CoreService.GetByIdAsync(ID ?? default);
                if (entity != null)
                    return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(entity), "Успешно");
                return Response<TDtoBase>.NotFound("Ресурс не найден");
            }
            catch(Exception ex) 
            {
                return Response<TDtoBase>.Error(ex.Message);
            }
        }

        public async Task<Response<TDtoBase>> UpdateItemAsync<TDto>(TDto dto) where TDto : IDataTransferObjectBaseUpdate<TEntity>
        {
            try
            {
                var validationResult = await ValidateItem<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<TDtoBase>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                var entity = await CoreService.UpdateItemAsync(Mapper.Map<TEntity>(dto));
                return Response<TDtoBase>.Ok(Mapper.Map<TDtoBase>(entity), "Успешно");
            }
            catch (Exception ex)
            {
                return Response<TDtoBase>.Error(ex.Message);
            }
        }

        private async ValueTask<ValidationResult> ValidateItem<TDto>(TDto dto) where TDto : IDataTransferObjectBase<TEntity>
        {
            var validator = ValidatorFactory.GetValidator<TDto>();
            return await validator.ValidateAsync(dto);
        }

        
    }
}
