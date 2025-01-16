namespace Nimap_Task.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty; // Default value to avoid nullability issues
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Make Category nullable
    }
}
