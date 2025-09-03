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

        public async Task<Group> CreateAsync(Group entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                await _dbcontext.Groups.AddAsync(entity, ct);
                await _dbcontext.CommitTransaction();
                return entity;
            }
            catch
            {
                await _dbcontext.RollbackTransaction();
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                await _dbcontext.Groups.Where(f => f.ID == id).ExecuteDeleteAsync(ct);
                await _dbcontext.CommitTransaction();
            }
            catch
            {
                await _dbcontext.RollbackTransaction();
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

        public async Task UpdateAsync(Group entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                _dbcontext.Groups.Update(entity);
                await _dbcontext.CommitTransaction();
            }
            catch
            {
                await _dbcontext.RollbackTransaction();
                throw;
            }
        }
    }
}
