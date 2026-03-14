using Application.Enums;
using Application.Responses;
using Application.Services.Base;
using Domain.Entities.Base;
using Microsoft.AspNetCore.Mvc;

namespace Math.Api.Controllers
{
    [ApiController]
    public abstract class CrudControllerBase<TEntity>(IServiceProvider services) : ControllerBase
        where TEntity : BaseEntity
    {
        protected ICrudServiceBase<TEntity> CrudService { get; } = services.GetRequiredService<ICrudServiceBase<TEntity>>();
        protected ActionResult<Response<TEntity>> ToActionResult(Response<TEntity> response)
        {
            return response.Status switch
            {
                ResponseStatus.Ok => Ok(response),
                _ => BadRequest(response)
            };
        }
    }
}

