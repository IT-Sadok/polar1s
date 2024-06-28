using eShop.Persistence.Filters;
using eShop.Persistence.Models;

namespace eShop.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order, OrderFilter>
    {
        public OrderRepository(ApplicationDbContext context)
            : base(context) { }

        public override IQueryable<Order> Get(OrderFilter filter)
        {
            IQueryable<Order> query = Context.Set<Order>();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
