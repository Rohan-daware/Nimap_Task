using Microsoft.AspNetCore.Mvc;
using Nimap_Task.Models;
using Nimap_Task.Repositories;

namespace Nimap_Task.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        // GET: Product/Create
        public IActionResult Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepo.AddProduct(product);
                return RedirectToAction("Display", new { id = product.CategoryId });
            }
            return View(product);
        }

        public IActionResult Display(int id, bool isStyled = false, bool hasPagination = false)
        {
            var category = _categoryRepo.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var products = _productRepo.GetProductsByCategoryId(id);

            // Ensure CategoryName is not null
            ViewBag.CategoryName = category?.CategoryName ?? "Unknown Category";

            // Set default values for ViewBag.IsStyled and ViewBag.HasPagination
            ViewBag.IsStyled = isStyled;
            ViewBag.HasPagination = hasPagination;

            return View("DisplayProducts", products); // Renders DisplayProducts view
        }
    }
}

