using ReadApi.Application.Contracts;
using ReadApi.Application.Interfaces.EfCore;
using ReadApi.Core.Entities;
using ReadApi.Infrastructure.Peristence.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReadApi.Infrastructure.Repository.EfCore
{
    public class UnitOfWorkCore : IUnitOfWorkCore
    {
        private readonly AppDbContext dbContext;

        public UnitOfWorkCore(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            ViolationRepositoryCore = new ViolationRepositoryCore(dbContext);
            //MediaRepositoryCore = new MediaRepositoryCore(dbContext);
        }
        public IViolationRepositoryCore ViolationRepositoryCore { get; private set; }
        //public IMediaRepositoryCore MediaRepositoryCore { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public double CalculateVehicleSpeed(double distance, double time)
        {
            // to do verify the units etc...
            return distance / time;
            
        }

        public void Dispose()
        {
           dbContext.Dispose();
        }
    }
}
