using SharedKernel.Contracts.Pagination;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal abstract class CrudService<TItem, TDetails, TFilter, TCreate, TUpdate>(HttpClient httpClient, EntityRoutes routes)
        : SelectionService<TItem, TDetails, TFilter>(httpClient, routes), ICrudService<TItem, TDetails, TFilter, TCreate, TUpdate>
        where TFilter : class, IQueryFilter, new()
    {
        public Task CreateAsync(TCreate request, CancellationToken cancellationToken = default) =>
            PostJsonAsync(Routes.Create, request, cancellationToken);

        public Task UpdateAsync(TUpdate request, CancellationToken cancellationToken = default) =>
            PutJsonAsync(Routes.Update, request, cancellationToken);

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
            SendDeleteAsync(WithId(Routes.Delete, id), cancellationToken);
    }
}
