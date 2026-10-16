namespace SharedKernel.Contracts.Pagination
{
    /// <summary>
    /// Фильтр сам перечисляет свои параметры для query-строки — клиенту не нужна рефлексия
    /// (в WASM при публикации метаданные обрезаются, и обход свойств может молча сломаться).
    /// Сервер этот метод не использует: ASP.NET биндит Filter.* сам.
    /// </summary>
    public interface IQueryFilter
    {
        IEnumerable<KeyValuePair<string, string?>> ToQuery();
    }
}
