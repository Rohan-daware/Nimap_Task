using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nimap_Task.Models;
using Nimap_Task.Repositories;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IProductRepository _productRepo;

    public CategoryController(ICategoryRepository categoryRepo, IProductRepository productRepo)
    {
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
    }

    // Display a list of categories
    public IActionResult Index()
    {
        var categories = _categoryRepo.GetAllCategories();
        return View(categories);
    }

    // Create a new category - GET
    public IActionResult Create()
    {
        return View(); // This should show the Add Category form.
    }

    // Create a new category - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.AddCategory(category); // Add category to the database
            return RedirectToAction("Index");   // Redirect back to category list
        }
        return View(category); // If validation fails, return to the form
    }



    //Edit
    public IActionResult Edit(int id)
    {
        var category = _categoryRepo.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.UpdateCategory(category); // Save changes
            return RedirectToAction("Index"); // Redirect to Index or another relevant page after update
        }
        return View(category); // If model is invalid, return to Edit page with validation messages
    }




    // Delete a category - GET
    public IActionResult Delete(int id)
    {
        var category = _categoryRepo.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    // Delete a category - POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _categoryRepo.GetCategoryById(id);
        if (category == null)
        {
            return NotFound();
        }

        _categoryRepo.DeleteCategory(id);
        return RedirectToAction("Index");
    }

    // Add Product to Category - GET
    public IActionResult AddProduct(int categoryId)
    {
        ViewBag.CategoryId = categoryId; 
        ViewBag.CategoryList = _categoryRepo.GetAllCategories()
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            });
        return View();
    }

    // Add Product to Category - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddProduct(Product product, int CategoryId)
    {
        int Temp = product.CategoryId;

        if (ModelState.IsValid)
        {
            _productRepo.AddProduct(product); // Save new product
            return RedirectToAction("DisplayProducts", new { categoryId = product.CategoryId });
        }
        String str = product.ProductName;
            int productId = product.ProductId;

        ViewBag.CategoryList = _categoryRepo.GetAllCategories()
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            });

        return View(product);
    }


    // Display products in a category
    public IActionResult DisplayProducts(int categoryId)
    {
        var category = _categoryRepo.GetCategoryById(categoryId);
        if (category == null)
        {
            return NotFound();
        }

        ViewBag.CategoryName = category.CategoryName;
        var products = _productRepo.GetProductsByCategoryId(categoryId);
        return View(products);
    }
}

