using Nimap_Task.Models;  

namespace Nimap_Task.ViewModels   
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; } 
        public int TotalPages { get; set; }  
        public int CurrentPage { get; set; } 
    }
}
