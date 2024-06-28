﻿using System.Linq.Expressions;

namespace eShop.Persistence.Interfaces
{
    public interface IRepository<TEntity, TFilter>
    {
        IQueryable<TEntity> Get(TFilter filter);
        Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
