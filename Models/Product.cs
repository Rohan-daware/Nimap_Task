//namespace Nimap_Task.Models
//{
//    public class Product
//    {
//        public int ProductId { get; set; }
//        public string ProductName { get; set; } = string.Empty; // Default value to avoid nullability issues
//        public int CategoryId { get; set; }
//        public Category? Category { get; set; } // Make Category nullable
//    }
//}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nimap_Task.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(10, ErrorMessage = "Maximum Length can not be more than 10")]
        [MinLength(1,ErrorMessage ="Minimum length can not be less than 1")]
        public string? ProductName { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
