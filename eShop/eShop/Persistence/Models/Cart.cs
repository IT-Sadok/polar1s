using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Persistence.Models
{
    public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public List<CartItem> Items { get; set; } = null!;
    }
}
