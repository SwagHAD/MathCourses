using Application.Commands.UpdateCommands;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace Application.Handlers.UpdateHandlers
{
    public sealed class UpdatePersonHandler(ISwagDbContext dbContext, IMapper mapper) : IRequestHandler<UpdatePersonCommand, Unit>
    {
        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await dbContext.Set<Person>().FirstOrDefaultAsync(p => p.Id == request.Data.Id, cancellationToken)
                ?? throw new NotFoundException(typeof(Person).GetDescription(), request.Data.Id);
            mapper.Map(request.Data, person);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
