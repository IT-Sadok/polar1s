using eShop.Infrastructure.Filters;
using eShop.Infrastructure.Persistance;
using eShop.Persistence.Models;

namespace eShop.Infrastructure.Repositories
{
    public class CartItemRepository : RepositoryBase<CartItem, CartItemFilter>
    {
        public CartItemRepository(ApplicationDbContext context)
            : base(context) { }

        public override IQueryable<CartItem> Get(CartItemFilter filter)
        {
            IQueryable<CartItem> query = Context.Set<CartItem>();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
