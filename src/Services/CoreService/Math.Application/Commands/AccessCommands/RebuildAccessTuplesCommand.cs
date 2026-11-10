using SharedKernel.Application.Commands;

namespace Application.Commands.AccessCommands
{
    /// <summary>
    /// Пересобирает выводимые кортежи из доменных таблиц. Нужна после ручных правок БД,
    /// массовых ExecuteUpdate, изменения правил проекции и при первом включении графа.
    /// Возвращает число созданных кортежей.
    /// </summary>
    public sealed record RebuildAccessTuplesCommand : ICommand<int>;
}
