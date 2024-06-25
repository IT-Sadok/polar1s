namespace eShop.Persistence.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public Cart Cart { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;
    }
}
