using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace StreamingService.Infrastructure.Postgres
{
    internal sealed class StreamDbContext(DbContextOptions<StreamDbContext> options) : BaseDbContext(options)
    {
    }
}
