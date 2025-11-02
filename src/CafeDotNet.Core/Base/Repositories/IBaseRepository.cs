using CafeDotNet.Core.Base.Entities;

namespace CafeDotNet.Core.Base.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task ActivateAsync(TEntity entity);
        Task DeactivateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
       
        Task<TEntity?> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
