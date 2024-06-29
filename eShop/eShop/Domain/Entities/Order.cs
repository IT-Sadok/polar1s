using eShop.Domain.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Persistence.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public List<CartItem> Items { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
