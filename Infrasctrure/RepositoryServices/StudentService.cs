using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class StudentService(IServiceProvider services) : ICoreRepository<Student>
    {
        private readonly IMathDbContext _dbcontext = services.GetRequiredService<IMathDbContext>();

        public Task<int> CountAsync(IQueryable<Student> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> CreateAsync(Student entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                await _dbcontext.Students.AddAsync(entity, ct);
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
                await _dbcontext.Students.Where(f => f.ID == id).ExecuteDeleteAsync(ct);
                await _dbcontext.CommitTransaction();
            }
            catch
            {
                await _dbcontext.RollbackTransaction();
                throw;
            }
        }

        public Task<Student?> FirstOrDefaultAsync(IQueryable<Student> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var student = await _dbcontext.Students.AsNoTracking().FirstOrDefaultAsync(f => f.ID == id, ct);
                return student ?? throw new KeyNotFoundException($"Student with ID {id} not found");
            }
            catch
            {
                throw;
            }
        }

        public Task<List<Student>> ToListAsync(IQueryable<Student> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Student>> ToPagedAsync(IQueryable<Student> query, int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Student entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                _dbcontext.Students.Update(entity);
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
