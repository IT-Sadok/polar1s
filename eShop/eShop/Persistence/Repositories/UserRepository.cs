using eShop.Persistence.Models;
using eShop.Persistence.Interfaces;
using eShop.Persistence.Filters;

namespace eShop.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User, UserFilter>
    {

        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<User> Get(UserFilter filter)
        {
            IQueryable<User> query = Context.Set<User>().AsQueryable();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
