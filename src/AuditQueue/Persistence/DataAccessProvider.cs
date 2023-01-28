using AuditQueue.DbModels;

namespace AuditQueue.Persistence
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly ViolationDbContext violationDbContext;

        public DataAccessProvider(ViolationDbContext violationDbContext)
        {
            this.violationDbContext = violationDbContext;
        }
        public async Task<int> AddNewViolationAsync(Violation violation)
        {
            await violationDbContext.ViolationSet.AddAsync(violation);
           return await violationDbContext.SaveChangesAsync();
        }
    }
}
