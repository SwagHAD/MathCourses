using DataMath;
using DataMath.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class StudentRepository(IMathContext ctx) : IGenericRepository<Student>
    {
        private readonly IMathContext _ctx = ctx;
        public async Task AddAsync(Student entity, CancellationToken cancellationToken = default)
        {
            await _ctx.Students.AddAsync(entity, cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Student entity, CancellationToken cancellationToken = default)
        {
            _ctx.Students.Remove(entity);
            await _ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _ctx.Students.Include(s => s.Groups).Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _ctx.Students.Include(s => s.Groups).ToListAsync(cancellationToken);
        }

        public async Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var student = await _ctx.Students.Include(s => s.Groups).FirstOrDefaultAsync(s => s.Id == id);
            if (student is null)
            {
                throw new InvalidOperationException($"Student with {id} doesn't exist!");
            }
            return student;
        }

        public async Task UpdateAsync(Student entity, CancellationToken cancellationToken = default)
        {
            _ctx.Students.Update(entity);
            await _ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
