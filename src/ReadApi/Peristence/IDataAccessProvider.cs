using ReadApi.Models;

namespace ReadApi.Peristence
{
    public interface IDataAccessProvider
    {
        Task<int> AddNewViolationAsync(Violation violation);
    }
}
