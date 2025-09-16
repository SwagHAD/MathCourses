using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class LessonService(IServiceProvider services) : ICoreRepository<Lesson>
    {
        private readonly IMathDbContext _dbcontext = services.GetRequiredService<IMathDbContext>();

        public Task<int> CountAsync(IQueryable<Lesson> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Lesson> CreateItemAsync(Lesson entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransactionAsync();
                await _dbcontext.Lessons.AddAsync(entity, ct);
                await _dbcontext.CommitTransactionAsync();
                return entity;
            }
            catch
            {
                await _dbcontext.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteItemAsync(int id, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransactionAsync();
                await _dbcontext.Lessons.Where(f => f.ID == id).ExecuteDeleteAsync(ct);
                await _dbcontext.CommitTransactionAsync();
            }
            catch
            {
                await _dbcontext.RollbackTransactionAsync();
                throw;
            }
        }

        public Task<Lesson?> FirstOrDefaultAsync(IQueryable<Lesson> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Lesson> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var lesson = await _dbcontext.Lessons.AsNoTracking().FirstOrDefaultAsync(f => f.ID == id);
                return lesson ?? throw new KeyNotFoundException($"Lesson with ID {id} not found");
            }
            catch
            {
                throw;
            }
        }

        public Task<List<Lesson>> ToListAsync(IQueryable<Lesson> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Lesson>> ToPagedAsync(IQueryable<Lesson> query, int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateItemAsync(Lesson entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransactionAsync();
                _dbcontext.Lessons.Update(entity);
                await _dbcontext.CommitTransactionAsync();
            }
            catch
            {
                await _dbcontext.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
