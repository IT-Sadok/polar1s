using eShop.Persistence.Filters;
using eShop.Persistence.Models;

namespace eShop.Persistence.Repositories
{
    public class CartItemRepository : RepositoryBase<CartItem, CartItemFilter>
    {
        public CartItemRepository(ApplicationDbContext context)
            : base(context) { }

        public override IQueryable<CartItem> Get(CartItemFilter filter)
        {
            IQueryable<CartItem> query = Context.Set<CartItem>().AsQueryable();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
