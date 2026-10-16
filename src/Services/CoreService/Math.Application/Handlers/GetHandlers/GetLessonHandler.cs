using Application.Queries.DefaultQueries;
using CoreService.Contracts.Lessons;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetLessonHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetLessonQuery, LessonResponse>
    {
        public async Task<LessonResponse> Handle(GetLessonQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Lesson>().AsNoTracking().Where(f => f.ID == request.Id).Select(f => new LessonResponse
            {
                Id = f.ID,
                Name = f.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Lesson).GetDescription(), request.Id);
        }
    }
}
