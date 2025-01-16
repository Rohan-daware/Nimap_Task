using Nimap_Task.Models;

namespace Nimap_Task.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(int page, int pageSize);
        int GetTotalProductsCount();
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id); // Marked as nullable to match the implementation
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);

        // Add this method to get products by category ID
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}
