using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class GroupService(IServiceProvider services) : ICoreRepository<Group>
    {
        private readonly IMathDbContext _dbcontext = services.GetRequiredService<IMathDbContext>();
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
