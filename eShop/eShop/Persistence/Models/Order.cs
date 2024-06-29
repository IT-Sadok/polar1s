using eShop.Persistence.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Persistence.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public List<CartItem> Items { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
