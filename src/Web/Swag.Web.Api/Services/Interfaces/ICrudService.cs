using SharedKernel.Contracts.Pagination;

namespace Swag.Web.Api.Services.Interfaces
{
    /// <summary>Чтение + создание, изменение, удаление. Для сущностей, где сервер ничего не возвращает на запись.</summary>
    public interface ICrudService<TItem, TDetails, TFilter, in TCreate, in TUpdate> : ISelectionService<TItem, TDetails, TFilter>
        where TFilter : class, IQueryFilter, new()
    {
        Task CreateAsync(TCreate request, CancellationToken cancellationToken = default);
        Task UpdateAsync(TUpdate request, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
