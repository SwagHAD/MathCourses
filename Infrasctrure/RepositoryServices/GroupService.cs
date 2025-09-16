using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class GroupService(IServiceProvider services) : ICoreRepository<Group>
    {
        private readonly IMathDbContext _dbcontext = services.GetRequiredService<IMathDbContext>();

        public Task<int> CountAsync(IQueryable<Group> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> CreateItemAsync(Group entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransactionAsync();
                await _dbcontext.Groups.AddAsync(entity, ct);
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
                await _dbcontext.Groups.Where(f => f.ID == id).ExecuteDeleteAsync(ct);
                await _dbcontext.CommitTransactionAsync();
            }
            catch
            {
                await _dbcontext.RollbackTransactionAsync();
                throw;
            }
        }

        public Task<Group?> FirstOrDefaultAsync(IQueryable<Group> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var group = await _dbcontext.Groups.AsNoTracking().FirstOrDefaultAsync(f => f.ID == id);
                return group ?? throw new KeyNotFoundException($"Group with ID {id} not found");
            }
            catch
            {
                throw;
            }
        }

        public Task<List<Group>> ToListAsync(IQueryable<Group> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Group>> ToPagedAsync(IQueryable<Group> query, int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateItemAsync(Group entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransactionAsync();
                _dbcontext.Groups.Update(entity);
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
