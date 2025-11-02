namespace CafeDotNet.Infra.Data.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    int Commit();
    Task<int> CommitAsync();
}
