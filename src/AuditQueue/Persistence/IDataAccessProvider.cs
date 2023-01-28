using AuditQueue.DbModels;

namespace AuditQueue.Persistence
{
    public  interface IDataAccessProvider
    {
        Task<int> AddNewViolationAsync(Violation violation);
    }
}
