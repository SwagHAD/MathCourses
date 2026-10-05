using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace Infrasctrure.Postgres;

internal sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : BaseDbContext(options)
{
}
