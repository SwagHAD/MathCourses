namespace SharedKernel.Contracts.Pagination
{
    /// <summary>Страница и количество записей, подходящих под фильтр (а не всех).</summary>
    public sealed record PageResult<T>(IReadOnlyList<T> Items, int TotalCount);
}
