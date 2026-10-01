using Application.Queries.Base;
using Application.Responses;
using Domain.Entities;
using Domain.Enums;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetLessonQuery : IQuery<DefaultLessonResponse>
    {
        public int Id { get; init; }
    }
}
