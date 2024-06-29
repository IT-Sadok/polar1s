using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Persistence.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public List<CartItem> Items { get; set; } = null!;
    }
}
