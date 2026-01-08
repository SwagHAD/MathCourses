using Application.DTO.Base;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.ExecuteActions
{
    public interface IExecuteConstructor<TEntity, TDto> where TEntity : BaseEntity where TDto : IDTOBaseUpdate<TEntity>
    {
        Expression<Func<UpdateSettersBuilder<TEntity>, UpdateSettersBuilder<Student>>> Execute(TDto dto);
    }
}
