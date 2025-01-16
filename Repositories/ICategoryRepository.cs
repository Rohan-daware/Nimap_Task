using Nimap_Task.Models;

namespace Nimap_Task.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category? GetCategoryById(int id); // Marked as nullable
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
    