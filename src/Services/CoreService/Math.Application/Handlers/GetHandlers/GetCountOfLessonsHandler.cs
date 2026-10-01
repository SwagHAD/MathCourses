using Application.Interfaces;
using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfLessonsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfLessonsQuery, int>
    {
        public async Task<int> Handle(GetCountOfLessonsQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Lessons.CountAsync(cancellationToken);
        }
    }
}
