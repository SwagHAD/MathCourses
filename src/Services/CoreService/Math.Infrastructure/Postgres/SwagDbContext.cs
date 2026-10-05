using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace Infrastructure.Postgres
{
    internal sealed class SwagDbContext(DbContextOptions<SwagDbContext> options) : BaseDbContext(options)
    {
    }
}
