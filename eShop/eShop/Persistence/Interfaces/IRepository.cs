using System.Linq.Expressions;

namespace eShop.Persistence.Interfaces
{
    public interface IRepository<TEntity, TFilter>
        where TEntity: class
    {
        Task<IQueryable<TEntity>> GetAsync(TFilter filter);
        // IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> condition);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
