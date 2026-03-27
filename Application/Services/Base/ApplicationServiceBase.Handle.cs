using Application.Base;
using Application.Responses.Base;
using Domain.Entities.Base;
using FluentValidation;
using MediatR;

namespace Application.Services.Base
{
    public partial class CrudServiceBase<TEntity> : ICrudServiceBase<TEntity>
        where TEntity : BaseEntity
    {
        private async Task<Response<TResponse>> Handle<TRequest, TResponse>(Func<Task<TEntity>> Action) 
            where TRequest : IRequest<TEntity> where TResponse : IBaseResponse<TEntity>
        {
            try
            {
                return Response<TResponse>.Ok(Mapper.Map<TResponse>(await Action()), "Успешно");
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
