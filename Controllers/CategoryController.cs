using Microsoft.AspNetCore.Mvc;
using Nimap_Task.Repositories;
using Nimap_Task.Models;
namespace Nimap_Task.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
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
            return View();
        }

        // Create a new category - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.AddCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Edit an existing category - GET
        public IActionResult Edit(int id)
        {
            var category = _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Edit an existing category - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
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
            _categoryRepo.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }


}
