using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;

namespace Application.Services.Base
{
    public partial class ApplicationServiceBase<TEntity> : IApplicationServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        private async Task<Response<TEntity>> Handle(Func<Task<TEntity>> Action, IDtoBase<TEntity> dto)
        {
            try
            {
                var validationResult = await ValidateItemAsync(dto);
                if (!validationResult.IsValid)
                    return Response<TEntity>.Fail(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
                var result = await Action();
                return Response<TEntity>.Ok(Mapper.Map<TEntity>(result), "Успешно");
            }
            catch (Exception ex)
            {
                return Response<TEntity>.Error(ex.Message);
            }
        }
        private async Task<Response<bool>> Handle(Func<Task> Action, IDtoBase<TEntity> dto)
        {
            try
            {
                var validationResult = await ValidateItemAsync(dto);
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
