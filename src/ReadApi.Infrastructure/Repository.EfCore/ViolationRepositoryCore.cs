using Microsoft.EntityFrameworkCore;
using ReadApi.Application.Contracts;
using ReadApi.Application.Interfaces.EfCore;
using ReadApi.Core.Entities;
using ReadApi.Infrastructure.Peristence.EfCore;

namespace ReadApi.Infrastructure.Repository.EfCore
{
    public class ViolationRepositoryCore : GenericRepositoryCore<Violation> , IViolationRepositoryCore
    {
        public ViolationRepositoryCore(AppDbContext context) : base(context)
        {
        }

        public List<Violation> GetViolations()
        {
            var data = _context.Violations.Include(v=>v.Medias).ToList();
            return data;
        }
    }
}
