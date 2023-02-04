using ReadApi.Core.Entities;

namespace ReadApi.Application.Interfaces.EfCore
{
    public interface IViolationRepositoryCore : IGenericRepositoryCore<Violation>
    {
        List<Violation> GetViolations();
    }
}
