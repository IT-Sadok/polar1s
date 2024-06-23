using eShop.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public int UserId { get; set; }

        public List<CartItem> Items { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
