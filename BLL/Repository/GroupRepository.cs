using DataMath;
using DataMath.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class GroupRepository(IMathContext ctx) : IGenericRepository<Group>
    {
        private readonly IMathContext _ctx = ctx;

        public async Task AddAsync(Group entity, CancellationToken cancellationToken = default)
        {
            await _ctx.Groups.AddAsync(entity, cancellationToken);
            await _ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Group entity, CancellationToken cancellationToken = default)
        {
            _ctx.Groups.Remove(entity);
            await _ctx.SaveChangesAsync(cancellationToken);
        }

        public async Task<ICollection<Group>> FindAsync(Expression<Func<Group, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _ctx.Groups.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<ICollection<Group>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _ctx.Groups.ToListAsync(cancellationToken);
        }

        public async Task<Group> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var group =  await _ctx.Groups.FindAsync(id, cancellationToken);
            if (group is null)
            {
                throw new InvalidOperationException($"Group with {id} doesn't exist!");
            }
            return group;
        }

        public async Task UpdateAsync(Group entity, CancellationToken cancellationToken = default)
        {
            _ctx.Groups.Update(entity);
            await _ctx.SaveChangesAsync(cancellationToken);
        }
    }

}
