using Nimap_Task.Models;
using Microsoft.EntityFrameworkCore;

namespace Nimap_Task.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Pagination with Category inclusion
        public IEnumerable<Product> GetProducts(int page, int pageSize)
        {
            return _context.Products
                .Include(p => p.Category) // Ensures Category data is included
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetTotalProductsCount() => _context.Products.Count();

        public IEnumerable<Product> GetAllProducts() => _context.Products.ToList();

        public Product? GetProductById(int id) => _context.Products.Find(id); // Return type nullable

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        // Add this method to get products by category ID
        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }
    }
}
