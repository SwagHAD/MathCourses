using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class CourseService(IServiceProvider service) : ICoreRepository<Course>
    {
        private IMathDbContext _dbcontext = service.GetRequiredService<IMathDbContext>();

        public Task<int> CountAsync(IQueryable<Course> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> CreateAsync(Course entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                await _dbcontext.Courses.AddAsync(entity, ct);
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
                await _dbcontext.Courses.Where(f => f.ID == id).ExecuteDeleteAsync(ct);
                await _dbcontext.CommitTransaction();
            }
            catch
            {
                await _dbcontext.RollbackTransaction();
                throw;
            }
        }

        public Task<Course?> FirstOrDefaultAsync(IQueryable<Course> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> GetByIdAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var course = await _dbcontext.Courses.AsNoTracking().FirstOrDefaultAsync(f => f.ID == id);
                return course ?? throw new KeyNotFoundException($"Course with ID {id} not found");
            }
            catch
            {
                throw;
            }
        }

        public Task<List<Course>> ToListAsync(IQueryable<Course> query, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Course>> ToPagedAsync(IQueryable<Course> query, int page, int pageSize, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Course entity, CancellationToken ct = default)
        {
            try
            {
                await _dbcontext.BeginTransaction();
                _dbcontext.Courses.Update(entity);
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
