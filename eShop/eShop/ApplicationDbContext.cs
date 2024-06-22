using Microsoft.EntityFrameworkCore;
using eShop.Models;

namespace eShop
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDataBase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(p => p.Status)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
