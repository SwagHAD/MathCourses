using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RepositoryServices
{
    public class CourseService(IServiceProvider service) : ICoreRepository<Course>
    {
        private IMathDbContext _dbcontext = service.GetRequiredService<IMathDbContext>();
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
