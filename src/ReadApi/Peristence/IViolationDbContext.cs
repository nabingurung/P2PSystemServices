using Microsoft.EntityFrameworkCore;
using ReadApi.Models;

namespace ReadApi.Peristence
{
    public interface IViolationDbContext
    {
        Task<int> AddViolationAsync(Violation violation);
    }
}
