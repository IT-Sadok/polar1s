using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Persistence.Models
{
    public class CartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;
        public string CartId { get; set; } = null!;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public string ProductId { get; set; } = null!;

        public int Quantity { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
        public string OrderId { get; set; } = null!;
    }
}
