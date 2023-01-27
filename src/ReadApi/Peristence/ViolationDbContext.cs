using Microsoft.EntityFrameworkCore;
using ReadApi.Models;

namespace ReadApi.Peristence
{
    public class ViolationDbContext : DbContext, IViolationDbContext
    {
        public ViolationDbContext(DbContextOptions<ViolationDbContext> dbContextOptions) : base (dbContextOptions){ }

        
        public Task<int> AddViolationAsync(Violation violation)
        {
            throw new NotImplementedException();
        }
    }
}
