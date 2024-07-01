using Microsoft.EntityFrameworkCore;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace eShop.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
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
            modelBuilder.Entity<ApplicationUser>()
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

            //SeedRoles(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        //private void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData
        //        (
        //            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "0", NormalizedName = "Admin" },
        //            new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" }
        //        );
        //}
    }
}
