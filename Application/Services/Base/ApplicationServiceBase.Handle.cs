using Application.DTO.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public partial class ApplicationServiceBase<TEntity> : IApplicationServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        private async Task<Response<TOut>> Handle<TDto, TOut>(Func<Task<TEntity>> Action, TDto dto) where TDto : IDTOBase<TEntity>
        {
            try
            {
                var validationResult = await ValidateItemAsync<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<TOut>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                var result = await Action();
                return Response<TOut>.Ok(Mapper.Map<TOut>(result), "Успешно");
            }
            catch (Exception ex)
            {
                return Response<TOut>.Error(ex.Message);
            }
        }
        private async Task<Response<bool>> Handle<TDto>(Func<Task> Action, TDto dto) where TDto : IDTOBase<TEntity>
        {
            try
            {
                var validationResult = await ValidateItemAsync<TDto>(dto);
                if (!validationResult.IsValid)
                    return Response<bool>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                await Action();
                return Response<bool>.Ok(true, "Успешно");
            }
            catch (Exception ex)
            {
                return Response<bool>.Error(ex.Message);
            }
        }
    }
}
