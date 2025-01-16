
using Microsoft.AspNetCore.Mvc;
using Nimap_Task.Models;
using Nimap_Task.Repositories;
using Nimap_Task.ViewModels;  // Import the correct namespace

namespace Nimap_Task.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index(int page = 1)
        {
            var pageSize = 10; // Customize this value as needed
            var products = _productRepository.GetProducts(page, pageSize);
            var totalProducts = _productRepository.GetTotalProductsCount();

            var viewModel = new ProductListViewModel
            {
                Products = products,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                CurrentPage = page
            };

            return View(viewModel);
        }

        // Other actions...
    }
}
