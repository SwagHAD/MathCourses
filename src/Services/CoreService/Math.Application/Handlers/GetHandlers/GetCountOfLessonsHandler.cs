using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfLessonsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfLessonsQuery, int>
    {
        public async Task<int> Handle(GetCountOfLessonsQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Lesson>().CountAsync(cancellationToken);
        }
    }
}
