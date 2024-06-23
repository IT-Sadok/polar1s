using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public int UserId { get; set; }

        public List<CartItem> Items { get; set; } = null!;
    }
}
