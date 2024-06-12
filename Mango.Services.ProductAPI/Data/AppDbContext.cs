using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Dell XPS 13",
                Description = "A powerful and compact laptop",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Laptop",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Apple MacBook Pro",
                Description = "High-performance laptop with sleek design",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Laptop",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Lenovo ThinkPad X1",
                Description = "Durable and reliable business laptop",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Laptop",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Asus ROG Zephyrus",
                Description = "Gaming laptop with high-end graphics",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Laptop",
            });

        }
    }
}
