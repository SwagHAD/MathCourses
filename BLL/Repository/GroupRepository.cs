using DataMath;
using DataMath.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Group>> FindAsync(Expression<Func<Group, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _ctx.Groups.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Group>> GetAllAsync(CancellationToken cancellationToken = default)
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
