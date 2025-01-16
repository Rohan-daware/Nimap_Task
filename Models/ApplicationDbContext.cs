using Microsoft.EntityFrameworkCore;

namespace Nimap_Task.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add some categories (seeding categories)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                new Category { CategoryId = 2, CategoryName = "Fashion" },
                new Category { CategoryId = 3, CategoryName = "Groceries" }
            );

            // Add some products (seeding products)
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Laptop", CategoryId = 1 },
                new Product { ProductId = 2, ProductName = "Smartphone", CategoryId = 1 },
                new Product { ProductId = 3, ProductName = "T-Shirt", CategoryId = 2 },
                new Product { ProductId = 4, ProductName = "Jeans", CategoryId = 2 },
                new Product { ProductId = 5, ProductName = "Milk", CategoryId = 3 },
                new Product { ProductId = 6, ProductName = "Eggs", CategoryId = 3 }
            );
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
