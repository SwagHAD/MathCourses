using DataMath.Entities;
using DataMath.Interfaces;


namespace DataMath.RealizationsInterfaces
{
    internal class GroupRepository(IMathContext ctx) : IGroupRepository
    {
        public async Task CreateAsync(Group group, CancellationToken cancellationToken = default)
        {
            await ctx.Groups.AddAsync(group, cancellationToken);
            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}
