using Domain.Interfaces.Data;

namespace Application.Services.UnitOfWork
{
    public sealed class UnitOfWork(IMathDbContext dbContext) : IUnitOfWork
    {
        private readonly IMathDbContext _dbContext = dbContext;
        public async Task BeginTransaction(CancellationToken ct)
        {
            
        }

        public Task Commit(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollBack(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
