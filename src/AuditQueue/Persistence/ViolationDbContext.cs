using AuditQueue.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AuditQueue.Persistence
{
    public class ViolationDbContext: DbContext
    {
        public ViolationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }
       
        public DbSet<Violation> ViolationSet { get; set;}
    }
}
