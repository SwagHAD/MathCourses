using Domain.Entities.Base;
using Domain.Entities.Pagination;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices.Base
{
    public class CoreServiceBase<T>(IServiceProvider services) : ICoreRepository<T> where T : BaseEntity
    {
        protected IMathDbContext mathDbContext = services.GetRequiredService<IMathDbContext>();
        public async Task<int> CountAsync(IQueryable<T> query, CancellationToken ct = default)
        {
            return await query.CountAsync(ct);
        }

        public async Task<T> CreateItemAsync(T entity, CancellationToken ct = default)
        {
            return await ExecuteItemWithTransactionAsync(() => CreateItemNoTransactionAsync(entity, ct));
        }

        public async Task<T> CreateItemNoTransactionAsync(T entity, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().AddAsync(entity);
            await mathDbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<T[]> CreateItemsAsync(T[] entities, CancellationToken ct = default)
        {
            return await ExecuteItemsWithTransactionAsync(() => CreateItemsNoTransactionAsync(entities, ct));
        }

        public async Task<T[]> CreateItemsNoTransactionAsync(T[] entities, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().AddRangeAsync(entities);
            await mathDbContext.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteItemAsync(int id, CancellationToken ct = default)
        {
            await mathDbContext.BeginTransactionAsync();
            try
            {
                await DeleteItemNoTransactionAsync(id, ct);
                await mathDbContext.CommitTransactionAsync();
            }
            catch
            {
                await mathDbContext.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteItemNoTransactionAsync(int id, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().Where(f => f.ID == id).ExecuteDeleteAsync();
        }

        public async Task DeleteItemsAsync(int[] entities, CancellationToken ct = default)
        {
            await mathDbContext.BeginTransactionAsync();
            try
            {
                await DeleteItemsNoTransactionAsync(entities, ct);
                await mathDbContext.CommitTransactionAsync();
            }
            catch
            {
                await mathDbContext.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteItemsNoTransactionAsync(int[] entities, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().Where(f => entities.Contains(f.ID)).ExecuteDeleteAsync();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await mathDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(f => f.ID == id) ?? null;
        }

        public async Task<PagedResult<T>> ToPagedAsync(IQueryable<T> query, int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateItemAsync(T entity, CancellationToken ct = default)
        {
            await mathDbContext.BeginTransactionAsync();
            try
            {
                await UpdateItemNoTransactionAsync(entity, ct);
                await mathDbContext.CommitTransactionAsync();
                return entity;
            }
            catch
            {
                await mathDbContext.CommitTransactionAsync();
                throw;
            }
        }

        public async Task<T> UpdateItemNoTransactionAsync(T entity, CancellationToken ct = default)
        {
            mathDbContext.Set<T>().Update(entity);
            await mathDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T[]> UpdateItemsAsync(T[] entities, CancellationToken ct = default)
        {
            await mathDbContext.BeginTransactionAsync();
            try
            {
                await UpdateItemsNoTransactionAsync(entities, ct);
                await mathDbContext.CommitTransactionAsync();
                return entities;
            }
            catch
            {
                await mathDbContext.CommitTransactionAsync();
                throw;
            }
        }

        public async Task<T[]> UpdateItemsNoTransactionAsync(T[] entities, CancellationToken ct = default)
        {
            mathDbContext.Set<T>().UpdateRange(entities);
            await mathDbContext.SaveChangesAsync();
            return entities;
        }
        private async Task<T> ExecuteItemWithTransactionAsync(Func<Task<T>> action, bool useTransaction = true)
        {
            if (useTransaction) await mathDbContext.BeginTransactionAsync();
            try
            {
                var result = await action();
                if (useTransaction) await mathDbContext.CommitTransactionAsync();
                return result;
            }
            catch
            {
                if (useTransaction) await mathDbContext.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task<T[]> ExecuteItemsWithTransactionAsync(Func<Task<T[]>> action, bool useTransaction = true)
        {
            if (useTransaction) await mathDbContext.BeginTransactionAsync();
            try
            {
                var result = await action();
                if (useTransaction) await mathDbContext.CommitTransactionAsync();
                return result;
            }
            catch
            {
                if (useTransaction) await mathDbContext.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ExecuteItemsWithTransactionAsync(Func<Task> action, bool useTransaction = true)
        {
            if (useTransaction) await mathDbContext.BeginTransactionAsync();
            try
            {
                await action();
                if (useTransaction) await mathDbContext.CommitTransactionAsync();
            }
            catch
            {
                if (useTransaction) await mathDbContext.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
