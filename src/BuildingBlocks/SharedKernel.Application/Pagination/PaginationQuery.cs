using SharedKernel.Application.Queries;
using SharedKernel.Contracts.Pagination;

namespace SharedKernel.Application.Pagination
{
    /// <summary>
    /// MediatR-обёртка над <see cref="PageRequest{TFilter}"/> из контрактов.
    /// Закрытый generic — по нему MediatR находит конкретный наследник <see cref="PaginationHandler{TEntity,TFilter,TResponse}"/>.
    /// </summary>
    public sealed record PaginationQuery<TFilter, TResponse>(PageRequest<TFilter> Request) : IQuery<PageResult<TResponse>>
        where TFilter : class, new();
}
