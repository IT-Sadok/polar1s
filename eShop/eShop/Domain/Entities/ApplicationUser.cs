using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eShop.Persistence.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Cart Cart { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;
    }
}
