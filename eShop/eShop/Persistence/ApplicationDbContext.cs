using Microsoft.EntityFrameworkCore;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eShop.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        //public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuration User table
            modelBuilder.Entity<User>()
                .HasIndex(p => p.UserName)
                .IsUnique();

            // Configuration Product table
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Category)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Category)
                .HasConversion<string>();

            // Configuration Order table
            modelBuilder.Entity<Order>()
                .Property(p => p.Status)
                .HasConversion<string>();

            // Configuration CartItem table
            modelBuilder.Entity<CartItem>()
                .Property(p => p.Quantity)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
