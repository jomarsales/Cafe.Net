using CafeDotNet.Core.Base.Entities;
using CafeDotNet.Core.Base.Repositories;
using CafeDotNet.Infra.Data.Common.Context;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IAsyncDisposable, IDisposable where TEntity : EntityBase
{
    protected readonly CafeDbContext _db;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(CafeDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _dbSet = _db.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _dbSet.AddAsync(entity);
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task ActivateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.Activate();
        _dbSet.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task DeactivateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.Deactivate();
        _dbSet.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<TEntity?> GetByIdAsync(object id)
    {
        ArgumentNullException.ThrowIfNull(id);

        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _db.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        _db.Dispose();
        GC.SuppressFinalize(this);
    }
}
