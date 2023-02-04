namespace ReadApi.Application
{
    public interface IUnitOfWork
    {
        IViolationRepository ViolationRepository { get; }
    }
}
