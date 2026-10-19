using SharedKernel.Contracts.Pagination;

namespace Swag.Web.Api.Services.Interfaces
{
    /// <summary>Чтение сущности: одна запись по id и страница по фильтру.</summary>
    /// <typeparam name="TItem">Строка списка.</typeparam>
    /// <typeparam name="TDetails">Карточка записи — может быть подробнее строки списка.</typeparam>
    /// <typeparam name="TFilter">Фильтр списка из контрактов сервиса.</typeparam>
    public interface ISelectionService<TItem, TDetails, TFilter>
        where TFilter : class, IQueryFilter, new()
    {
        Task<TDetails> GetItemAsync(int id, CancellationToken cancellationToken = default);
        Task<PageResult<TItem>> GetItemsAsync(PageRequest<TFilter> request, CancellationToken cancellationToken = default);
    }
}
