using Domain.Entities.Base;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices.Base
{
    /// <summary>
    /// Базовый серсис CRUD
    /// </summary>
    /// <typeparam name="T">Базовый тип, от которого наследуются все Entity</typeparam>
    /// <param name="CoreServiceBase"></param>
    public class CoreServiceBase<T>(IServiceProvider services) : ICoreRepository<T> where T : BaseEntity
    {
        protected IMathDbContext mathDbContext = services.GetRequiredService<IMathDbContext>();
        public async ValueTask<int> CountAsync(IQueryable<T> query, CancellationToken ct = default)
        {
            return await query.CountAsync(ct);
        }

        public async Task<T> CreateItemAsync(T entity, CancellationToken ct = default)
        {
            return await ExecuteItemWithTransactionAsync(() => CreateItemNoTransactionAsync(entity, ct), true, ct);
        }

        public async Task<T> CreateItemNoTransactionAsync(T entity, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().AddAsync(entity, ct);
            await mathDbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<T[]> CreateItemsAsync(T[] entities, CancellationToken ct = default)
        {
            return await ExecuteItemWithTransactionAsync(() => CreateItemsNoTransactionAsync(entities, ct), true, ct);
        }

        public async Task<T[]> CreateItemsNoTransactionAsync(T[] entities, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().AddRangeAsync(entities);
            await mathDbContext.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteItemAsync(int id, CancellationToken ct = default)
        {
            await ExecuteItemWithTransactionAsync(() => DeleteItemNoTransactionAsync(id, ct), true, ct);
        }

        public async Task DeleteItemNoTransactionAsync(int id, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().Where(f => f.ID == id).ExecuteDeleteAsync();
        }

        public async Task DeleteItemsAsync(int[] entities, CancellationToken ct = default)
        {
            await ExecuteItemWithTransactionAsync(() => DeleteItemsNoTransactionAsync(entities, ct), true, ct);
        }

        public async Task DeleteItemsNoTransactionAsync(int[] entities, CancellationToken ct = default)
        {
            await mathDbContext.Set<T>().Where(f => entities.Contains(f.ID)).ExecuteDeleteAsync();
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await mathDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(f => f.ID == id) ?? null;
        }


        public async Task<T> UpdateItemAsync(T entity, CancellationToken ct = default)
        {
            return await ExecuteItemWithTransactionAsync(() =>  UpdateItemNoTransactionAsync(entity, ct), true, ct);
        }

        public async Task<T> UpdateItemNoTransactionAsync(T entity, CancellationToken ct = default)
        {
            mathDbContext.Set<T>().Update(entity);
            await mathDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T[]> UpdateItemsAsync(T[] entities, CancellationToken ct = default)
        {
            return await ExecuteItemWithTransactionAsync(() => UpdateItemsNoTransactionAsync(entities, ct), true, ct);
        }

        public async Task<T[]> UpdateItemsNoTransactionAsync(T[] entities, CancellationToken ct = default)
        {
            mathDbContext.Set<T>().UpdateRange(entities);
            await mathDbContext.SaveChangesAsync();
            return entities;
        }
        private async Task<T> ExecuteItemWithTransactionAsync(Func<Task<T>> action, bool useTransaction, CancellationToken cancellation = default)
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
        private async Task<T[]> ExecuteItemWithTransactionAsync(Func<Task<T[]>> action, bool useTransaction, CancellationToken cancellation = default)
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
        private async Task ExecuteItemWithTransactionAsync(Func<Task> action, bool useTransaction, CancellationToken cancellation = default)
        {
            if (useTransaction) await mathDbContext.BeginTransactionAsync(cancellation);
            try
            {
                await action();
                if (useTransaction) await mathDbContext.CommitTransactionAsync(cancellation);
            }
            catch
            {
                if (useTransaction) await mathDbContext.RollbackTransactionAsync(cancellation);
                throw;
            }
        }
    }
}
