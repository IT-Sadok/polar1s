﻿using eShop.Infrastructure.Filters;
using eShop.Infrastructure.Persistance;
using eShop.Persistence.Models;

namespace eShop.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product, ProductFilter>
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context) { }

        public override IQueryable<Product> Get(ProductFilter filter)
        {
            IQueryable<Product> query = Context.Set<Product>();

            if (filter.Ids != null && filter.Ids.Any())
            {
                query = query.Where(user => filter.Ids.Contains(user.Id));
            }

            return query;
        }
    }
}
