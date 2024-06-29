using eShop.Persistence.Models;
using eShop.Persistence.Filters;
using eShop.Infrastructure.Persistance;

namespace eShop.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser, UserFilter>
    {

        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override IQueryable<ApplicationUser> Get(UserFilter filter)
        {
            IQueryable<ApplicationUser> query = Context.Set<ApplicationUser>().AsQueryable();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
