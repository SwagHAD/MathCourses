using Application.Commands.DeleteCommands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace Application.Handlers.DeleteHandlers
{
    public sealed class DeleteCourseHandler(ISwagDbContext dbContext) : IRequestHandler<DeleteCourseCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            // Один запрос вместо Any + Delete: заодно нет гонки между проверкой и удалением
            var deleted = await dbContext.Set<Course>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
            if (deleted == 0)
                throw new NotFoundException(typeof(Course).GetDescription(), request.Id);
            return Unit.Value;
        }
    }
}
