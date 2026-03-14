using Application.Command.Base;
using Application.Responses;
using Domain.Entities.Base;
using FluentValidation;

namespace Application.Services.Base
{
    public partial class CrudServiceBase<TEntity> : ICrudServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        private async Task<Response<TResponse>> Handle<TRequest, TResponse>(Func<Task<TEntity>> Action, TRequest dto) 
            where TRequest : ICommandBase<TEntity> where TResponse : ICommandBase<TEntity>
        {
            try
            {
                var result = await Action();
                return Response<TResponse>.Ok(Mapper.Map<TResponse>(result), "Успешно");
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors
                    .Where(e => e is not null)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Response<TResponse>.Fail(errors);
            }
            catch (Exception ex)
            {
                return Response<TResponse>.Error(ex.Message);
            }
        }
    }
}
