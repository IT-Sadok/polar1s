﻿using eShop.Persistence.Filters;
using eShop.Persistence.Models;

namespace eShop.Persistence.Repositories
{
    public class CartRepository : RepositoryBase<Cart, CartFilter>
    {
        public CartRepository(ApplicationDbContext context)
            : base(context) { }

        public override IQueryable<Cart> Get(CartFilter filter)
        {
            IQueryable<Cart> query = Context.Set<Cart>().AsQueryable();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
