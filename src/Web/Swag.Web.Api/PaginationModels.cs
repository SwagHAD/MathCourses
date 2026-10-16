namespace Swag.Web.Api
{
    // Восстановлено дословно из присланного в чат кода: типы использует ISelectionService.
    // Аналоги из контрактов: SharedKernel.Contracts.Pagination.PageResult / PageRequest.
    public sealed record PaginationResult<T>(T[] Items, int TotalCount)
    {
        public readonly T[] _items = Items;
        public readonly int TotalCount = TotalCount;
    }

    public sealed record PaginationQeury(int PageCount, int PageSize)
    {
        public readonly int _pageCount = PageCount;
        public readonly int _pageSize = PageSize;
        public Dictionary<string, string> _searchStrings = [];
    }
}
