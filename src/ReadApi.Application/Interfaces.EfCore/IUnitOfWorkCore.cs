using ReadApi.Application.Contracts;
using ReadApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadApi.Application.Interfaces.EfCore
{
    public interface IUnitOfWorkCore : IDisposable
    {
        IViolationRepositoryCore ViolationRepositoryCore { get; }
       // IMediaRepositoryCore MediaRepositoryCore { get; }       

        Task<int> CompleteAsync();

        double CalculateVehicleSpeed(double distance, double time);
    }
}
