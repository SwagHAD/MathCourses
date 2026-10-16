namespace Swag.Web.Api.Services.Interfaces
{
    public interface ISelectionService<TResult> : IDisposable, IAsyncDisposable
        where TResult : class
    {
        Task<TResult> GetItemAsync(int id);
        Task<PaginationResult<TResult>> GetItemsAsync(PaginationQeury paginationQeury);
    }
}
