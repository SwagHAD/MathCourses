using DataMath.Entities;

namespace DataMath.Interfaces
{
    public interface IGroupRepository
    {
        Task CreateAsync(Group group, CancellationToken cancellationToken = default);
    }
}
