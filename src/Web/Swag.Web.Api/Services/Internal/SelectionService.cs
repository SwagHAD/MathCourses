using SharedKernel.Contracts.Pagination;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal abstract class SelectionService<TItem, TDetails, TFilter>(HttpClient httpClient, EntityRoutes routes)
        : ApiClientBase(httpClient), ISelectionService<TItem, TDetails, TFilter>
        where TFilter : class, IQueryFilter, new()
    {
        protected EntityRoutes Routes => routes;

        public Task<TDetails> GetItemAsync(int id, CancellationToken cancellationToken = default) =>
            GetJsonAsync<TDetails>(WithId(routes.Get, id), cancellationToken);

        public Task<PageResult<TItem>> GetItemsAsync(PageRequest<TFilter> request, CancellationToken cancellationToken = default) =>
            GetJsonAsync<PageResult<TItem>>(QueryString.ForPage(routes.GetPage, request), cancellationToken);
    }
}
