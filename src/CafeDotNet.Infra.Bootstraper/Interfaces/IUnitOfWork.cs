namespace CafeDotNet.Infra.Bootstraper.Interfaces;

public interface IUnitOfWork : IDisposable
{
    int Commit();
    Task<int> CommitAsync();
}
