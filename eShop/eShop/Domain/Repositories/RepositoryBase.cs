using eShop.Application.Abstractions;
using eShop.Infrastructure.Persistance;

namespace eShop.Persistence.Repositories
{
    public abstract class RepositoryBase<TEntity, TFilter> : IRepository<TEntity, TFilter>
        where TEntity : class
    {
        protected ApplicationDbContext Context { get; set; }

        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public abstract IQueryable<TEntity> Get(TFilter filter);

        public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
