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
    public abstract class ApplicationServiceBase<TEntity, TDtoBase>(IServiceProvider services) : IApplicationServiceBase<TEntity, TDtoBase> 
        where TEntity : BaseEntity where TDtoBase : IDataTransferObjectBase<TEntity>
    {
        protected IMathDbContext DbContext { get; } = services.GetRequiredService<IMathDbContext>();
        protected ICoreRepository<TEntity> CoreService { get; } = services.GetRequiredService<ICoreRepository<TEntity>>();
        protected IValidatorFactoryBase ValidatorFactory { get; } = services.GetRequiredService<IValidatorFactoryBase>();
        protected IMapper Mapper { get; } = services.GetRequiredService<IMapper>();

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

        private Task<ValidationResult> ValidateItem<TDto>(TDto dto) where TDto : IDataTransferObjectBase<TEntity>
        {
            var validator = ValidatorFactory.GetValidator<TDto>();
            var validationresult = validator.Validate(dto);
            return Task.FromResult(validationresult);
        }
    }
}
