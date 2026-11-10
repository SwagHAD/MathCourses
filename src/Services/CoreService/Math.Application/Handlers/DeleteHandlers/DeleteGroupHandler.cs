using Application.Commands.DeleteCommands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace Application.Handlers.DeleteHandlers
{
    /// <summary>
    /// Через трекер, а не ExecuteDelete: участники загружаются и удаляются каскадом в памяти,
    /// поэтому AccessTupleInterceptor видит их удаление и снимает кортежи group#learner/instructor.
    /// При ExecuteDelete каскад сделала бы БД, и кортежи остались бы висеть.
    /// </summary>
    public sealed class DeleteGroupHandler(ISwagDbContext dbContext) : IRequestHandler<DeleteGroupCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await dbContext.Set<Group>()
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(typeof(Group).GetDescription(), request.Id);

            dbContext.Set<Group>().Remove(group);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
