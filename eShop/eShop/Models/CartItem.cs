using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; } = null!;
        public int CartId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }
    }
}
