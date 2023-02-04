using ReadApi.Application;

namespace ReadApi.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IViolationRepository violationRepository)
        {
            ViolationRepository = violationRepository;
        }

        public IViolationRepository ViolationRepository { get; }

    }
}
