using Nimap_Task.Models;
using Nimap_Task.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
    }

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int name)
    {
        var category = _context.Categories.FirstOrDefault(c => c.CategoryId == name);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
