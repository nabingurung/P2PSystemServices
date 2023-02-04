using Microsoft.EntityFrameworkCore;
using ReadApi.Core.Entities;

namespace ReadApi.Application
{
    public interface IAppDbContext
    {
        public DbSet<Violation> Violations { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
