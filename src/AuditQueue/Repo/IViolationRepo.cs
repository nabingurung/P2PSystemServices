using System;
using AuditQueue.DbModels;

namespace AuditQueue.Repo
{
	public interface IViolationRepo
	{
        Task<IEnumerable<Violation>> GetAllAsync();

        Task<Violation> GetAsync(int id);

        Task<int> AddAsync(Violation violation);

        Task<int> DeleteAsync(int id);
    }
}

