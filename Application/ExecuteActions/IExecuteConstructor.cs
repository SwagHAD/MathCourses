using Application.DTO.Base;
using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.ExecuteActions
{
    public interface IExecuteConstructor<TEntity, TDto> where TEntity : BaseEntity where TDto : IDataTransferObjectBaseUpdate<TEntity>
    {
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<Student>>> Execute(TDto dto);
    }
}
