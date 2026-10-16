using CoreService.Contracts.Lessons;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetLessonQuery : IQuery<LessonResponse>
    {
        public int Id { get; init; }
    }
}
