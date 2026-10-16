using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Contracts.Pagination;

namespace SharedKernel.Application.Pagination
{
    /// <summary>
    /// Общая пагинация: фильтр → количество → сортировка → страница → проекция.
    /// Наследник описывает только свои фильтры, сортировки и проекцию в DTO.
    /// </summary>
    public abstract class PaginationHandler<TEntity, TFilter, TResponse>(ISwagDbContext dbContext)
        : IRequestHandler<PaginationQuery<TFilter, TResponse>, PageResult<TResponse>>
        where TEntity : class
        where TFilter : class, new()
    {
        private const int MaxPageSize = 100;

        /// <summary>Белый список фильтров: применяется только то, что явно описано.</summary>
        protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);

        /// <summary>Белый список сортировок. Создавайте со <see cref="StringComparer.OrdinalIgnoreCase"/>.</summary>
        protected abstract IReadOnlyDictionary<string, Expression<Func<TEntity, object>>> SortKeys { get; }

        /// <summary>Сортировка по умолчанию; она же добивает любую сортировку, чтобы страницы были стабильными.</summary>
        protected abstract Expression<Func<TEntity, object>> DefaultSort { get; }

        protected abstract Expression<Func<TEntity, TResponse>> Projection { get; }

        public async Task<PageResult<TResponse>> Handle(PaginationQuery<TFilter, TResponse> query,
            CancellationToken cancellationToken)
        {
            var request = query.Request;
            Validate(request);

            var filtered = ApplyFilter(dbContext.Set<TEntity>().AsNoTracking(), request.Filter);
            var totalCount = await filtered.CountAsync(cancellationToken);

            var sort = request.SortBy is null ? DefaultSort : SortKeys[request.SortBy];
            var ordered = (request.Descending ? filtered.OrderByDescending(sort) : filtered.OrderBy(sort))
                .ThenBy(DefaultSort);

            var items = await ordered
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(Projection)
                .ToArrayAsync(cancellationToken);

            return new PageResult<TResponse>(items, totalCount);
        }

        /// <summary>
        /// Проверка здесь, а не валидатором: AddValidatorsFromAssembly не регистрирует open generic,
        /// а белый список сортировок есть только у обработчика.
        /// </summary>
        private void Validate(PageRequest<TFilter> request)
        {
            var failures = new List<ValidationFailure>();
            if (request.Page < 1)
                failures.Add(new(nameof(request.Page), "Номер страницы должен быть больше 0"));
            if (request.PageSize is < 1 or > MaxPageSize)
                failures.Add(new(nameof(request.PageSize), $"Размер страницы должен быть от 1 до {MaxPageSize}"));
            if (request.SortBy is not null && !SortKeys.ContainsKey(request.SortBy))
                failures.Add(new(nameof(request.SortBy), $"Сортировка возможна по: {string.Join(", ", SortKeys.Keys)}"));
            if (failures.Count > 0)
                throw new ValidationException(failures);
        }
    }
}
