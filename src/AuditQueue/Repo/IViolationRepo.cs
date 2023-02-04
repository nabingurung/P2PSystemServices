using System;
using AuditQueue.DbModels;
using AuditQueue.Models;

namespace AuditQueue.Repo
{
	public interface IViolationRepo
	{
        Task<IEnumerable<Violation>> GetAllAsync();

        Task<Violation> GetAsync(long id);

        Task<int> AddAsync(Violation violation);

        Task<int> DeleteAsync(long id);

        Task UpdateAsync(long id);

        Task GetAlprImageAsync(AlprDataDto alprDataDto);
    }
}

