using eShop.Persistence.Models;
using eShop.Persistence.Interfaces;

namespace eShop.Persistence
{
    public class UserRepository : RepositoryBase<User, IFilter>
    {
        
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<User>> GetAsync(IFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
