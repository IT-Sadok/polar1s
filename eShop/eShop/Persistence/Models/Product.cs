using eShop.Persistence.Models.Enums;

namespace eShop.Persistence.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public ProductCategory Category { get; set; }
    }
}
