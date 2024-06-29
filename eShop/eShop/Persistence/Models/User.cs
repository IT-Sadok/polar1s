using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eShop.Persistence.Models
{
    public class User : IdentityUser
    {
        //public override int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public override string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public override string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public override string? PasswordHash { get; set; }
        public Cart Cart { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;
    }
}
