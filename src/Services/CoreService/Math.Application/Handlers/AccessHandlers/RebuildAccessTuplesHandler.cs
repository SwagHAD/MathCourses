using Application.Access;
using Application.Commands.AccessCommands;
using Domain.Access;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.AccessHandlers
{
    /// <summary>
    /// Удаляет кортежи, принадлежащие проекциям, и создаёт их заново.
    /// Вся команда — одна транзакция (TransactionBehavior): читатели видят либо старый граф, либо новый.
    /// Явные выдачи и реплики не трогаются.
    /// </summary>
    internal sealed class RebuildAccessTuplesHandler(ISwagDbContext dbContext, IEnumerable<IAccessTupleProjection> projections)
        : IRequestHandler<RebuildAccessTuplesCommand, int>
    {
        private const int BatchSize = 1000;

        public async Task<int> Handle(RebuildAccessTuplesCommand request, CancellationToken cancellationToken)
        {
            var created = 0;
            foreach (var projection in projections)
            {
                foreach (var owned in projection.OwnedRelations)
                {
                    await dbContext.Set<AccessTuple>()
                        .Where(t => !t.IsReplica && t.ObjectType == owned.ObjectType && t.Relation == owned.Relation)
                        .ExecuteDeleteAsync(cancellationToken);
                }

                var keys = await projection.ProjectAllAsync(dbContext, cancellationToken);
                foreach (var batch in keys.Distinct().Chunk(BatchSize))
                {
                    await dbContext.Set<AccessTuple>().AddRangeAsync(batch.Select(AccessTuple.Create), cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    created += batch.Length;
                }
            }
            return created;
        }
    }
}
