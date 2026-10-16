namespace SharedKernel.Contracts.Pagination
{
    /// <summary>
    /// Запрос страницы. В GET передаётся query-строкой:
    /// ?Page=1&amp;PageSize=20&amp;SortBy=name&amp;Descending=false&amp;Filter.Name=иван
    /// </summary>
    public sealed record PageRequest<TFilter> where TFilter : class, new()
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
        /// <summary>Ключ сортировки из белого списка сущности; null — по Id.</summary>
        public string? SortBy { get; init; }
        public bool Descending { get; init; }
        public TFilter Filter { get; init; } = new();
    }
}
