using CafeDotNet.Infra.Data.Common.Context;
using CafeDotNet.Infra.Data.Common.Interfaces;

namespace CafeDotNet.Infra.Bootstraper.Services;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly CafeDbContext _context;

    public UnitOfWork(CafeDbContext context)
    {
        _context = context;
    }

    public int Commit()
    {
        var affectedEntries = _context.SaveChanges();
       
        return affectedEntries;
    }

    public async Task<int> CommitAsync()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var affectedEntries = await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
            
            return affectedEntries;
        }
        catch
        {
            await transaction.RollbackAsync();
            
            throw;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
