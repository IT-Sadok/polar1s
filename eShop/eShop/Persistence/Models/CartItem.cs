using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Persistence.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;
        public int CartId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }
    }
}
