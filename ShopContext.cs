using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Models;

namespace ShopForHome.Api.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<UserCoupon> UserCoupons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
    new User
    {
        Id = 1,
        FullName = "Admin User",
        Email = "admin@shop.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // ✅ hash password
        Role = "Admin"
    }
);


            // ✅ Order → OrderItem relationship
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99M, Rating = 4.5M, ImageUrl = "https://example.com/laptop.jpg", Category = "Electronics", Stock = 50 },
                new Product { Id = 2, Name = "Smartphone", Price = 699.99M, Rating = 4.3M, ImageUrl = "https://example.com/smartphone.jpg", Category = "Electronics", Stock = 100 },
                new Product { Id = 3, Name = "Headphones", Price = 199.99M, Rating = 4.0M, ImageUrl = "https://example.com/headphones.jpg", Category = "Accessories", Stock = 200 },
                new Product { Id = 4, Name = "Coffee Maker", Price = 49.99M, Rating = 3.8M, ImageUrl = "https://example.com/coffeemaker.jpg", Category = "Home Appliances", Stock = 80 },
                new Product { Id = 5, Name = "Gaming Console", Price = 399.99M, Rating = 4.7M, ImageUrl = "https://example.com/console.jpg", Category = "Electronics", Stock = 30 }
            );
            // coupon
            new Coupon
            {
                Id = 1,
                Code = "WELCOME10",
                DiscountPercentage = 10,
                // ValidFrom = DateTime.Now,
                // ValidTo = DateTime.Now.AddMonths(6),
                ExpiryDate = DateTime.Now.AddMonths(6),
                // Active = true
            };
            new Coupon
            {
                Id = 2,
                Code = "FESTIVE20",
                DiscountPercentage = 20,
                // ValidFrom = DateTime.Now,
                // ValidTo = DateTime.Now.AddMonths(3),
                ExpiryDate = DateTime.Now.AddMonths(3),
                // Active = true
            };
        }
        
    }
}