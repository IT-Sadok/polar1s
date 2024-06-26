using eShop.Persistence.Interfaces;

namespace eShop.Persistence
{
    public abstract class RepositoryBase<TEntity, TFilter> : IRepository<TEntity, TFilter>
        where TEntity : class
    {
        protected ApplicationDbContext Context { get; set; }

        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public abstract Task<IQueryable<TEntity>> GetAsync(TFilter filter);

        //public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> condition)
        //{
        //    return Context.Set<TEntity>().Where(condition);
        //}


        public async Task CreateAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
