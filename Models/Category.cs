//namespace Nimap_Task.Models
//{
//    public class Category
//    {
//        public int CategoryId { get; set; }
//        public string CategoryName { get; set; }
//    }
//}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nimap_Task.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
