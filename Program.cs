using Microsoft.EntityFrameworkCore;
using Nimap_Task.Models;
using Nimap_Task.Repositories;

namespace Nimap_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Add DbContext configuration for Entity Framework Core
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories in the dependency injection container
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            var app = builder.Build();

            // Step 2: Run console logic within a scope
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                ICategoryRepository categoryRepository = new CategoryRepository(context);
                IProductRepository productRepository = new ProductRepository(context);

                while (true)
                {
                    // Step 3: Show categories and let the user select one
                    Console.WriteLine("Select a category:");
                    var categories = categoryRepository.GetAllCategories();
                    int index = 1;
                    foreach (var category in categories)
                    {
                        Console.WriteLine($"{index}. {category.CategoryName}");
                        index++;
                    }

                    int categoryChoice;
                    if (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 1 || categoryChoice > categories.Count())
                    {
                        Console.WriteLine("Invalid category selection. Please try again.");
                        continue;
                    }

                    var selectedCategory = categories.ElementAt(categoryChoice - 1);
                    Console.WriteLine($"You selected: {selectedCategory.CategoryName}");

                    // Step 4: Provide operation options
                    bool keepOperating = true;
                    while (keepOperating)
                    {
                        Console.WriteLine("Select an operation:");
                        Console.WriteLine("1. Add Product");
                        Console.WriteLine("2. Edit Product");
                        Console.WriteLine("3. Delete Product");
                        Console.WriteLine("4. Display Products");
                        Console.WriteLine("5. Back to Categories");

                        int operationChoice;
                        if (!int.TryParse(Console.ReadLine(), out operationChoice) || operationChoice < 1 || operationChoice > 5)
                        {
                            Console.WriteLine("Invalid operation choice. Please try again.");
                            continue;
                        }

                        switch (operationChoice)
                        {
                            case 1:
                                // Add Product
                                Console.WriteLine("Enter Product Name:");
                                string productName = Console.ReadLine();
                                Product newProduct = new Product
                                {
                                    ProductName = productName,
                                    CategoryId = selectedCategory.CategoryId
                                };
                                productRepository.AddProduct(newProduct);
                                Console.WriteLine("Product added successfully!");
                                break;

                            case 2:
                                // Edit Product
                                Console.WriteLine("Enter Product ID to Edit:");
                                int editProductId = Convert.ToInt32(Console.ReadLine());
                                var productToEdit = productRepository.GetProductById(editProductId);
                                if (productToEdit != null && productToEdit.CategoryId == selectedCategory.CategoryId)
                                {
                                    Console.WriteLine("Enter new Product Name:");
                                    string newProductName = Console.ReadLine();
                                    productToEdit.ProductName = newProductName;
                                    productRepository.UpdateProduct(productToEdit);
                                    Console.WriteLine("Product updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found in this category.");
                                }
                                break;

                            case 3:
                                // Delete Product
                                Console.WriteLine("Enter Product ID to Delete:");
                                int deleteProductId = Convert.ToInt32(Console.ReadLine());
                                var productToDelete = productRepository.GetProductById(deleteProductId);
                                if (productToDelete != null && productToDelete.CategoryId == selectedCategory.CategoryId)
                                {
                                    productRepository.DeleteProduct(deleteProductId);
                                    Console.WriteLine("Product deleted successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found in this category.");
                                }
                                break;

                            case 4:
                                // Display Products
                                Console.WriteLine($"Products in {selectedCategory.CategoryName}:");
                                var productsInCategory = productRepository.GetProductsByCategoryId(selectedCategory.CategoryId);
                                if (productsInCategory.Any())
                                {
                                    foreach (var product in productsInCategory)
                                    {
                                        Console.WriteLine($"    Product ID: {product.ProductId}, Name: {product.ProductName}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No products found in this category.");
                                }
                                break;

                            case 5:
                                // Exit
                                Console.WriteLine("Exiting category operations.");
                                keepOperating = false;
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                }
            }

            // Step 6: Configure HTTP request pipeline for the web app
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run(); // Starts the web application
        }
    }
}
