using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class TeacherService(IServiceProvider services) : ICoreRepository<Teacher>
    {
        private readonly IMathDbContext _dbcontext = services.GetRequiredService<IMathDbContext>();
        public async Task<Teacher> CreateAsync(Teacher entity, CancellationToken ct = default)
        {
            await _dbcontext.BeginTransaction();
            try
            {
                await _dbcontext.Teachers.AddAsync(entity, ct);
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
            await _dbcontext.BeginTransaction();
            try
            {
                await _dbcontext.Teachers.Where(f => f.ID == id).ExecuteDeleteAsync(ct);
                await _dbcontext.CommitTransaction();
            }
            catch
            {
                await _dbcontext.RollbackTransaction();
                throw;
            }
        }

        public Task<Teacher> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var teacher = _dbcontext.Teachers.AsNoTracking().FirstOrDefault(f => f.ID == id);
                return Task.FromResult(teacher ?? throw new KeyNotFoundException($"Teacher with ID {id} not found"));
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(Teacher entity, CancellationToken ct = default)
        {
            await _dbcontext.BeginTransaction();
            try
            {
                _dbcontext.Teachers.Update(entity);
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
