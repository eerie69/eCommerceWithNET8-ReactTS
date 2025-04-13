using api.Dtos.Reviews;
using DemoShop.Dtos.Product;
using System.ComponentModel.DataAnnotations;

namespace DemoShop.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Category name cannot be over 20 characters")]
        public string? CategoryName { get; set; } = string.Empty;
        public List<ProductDto>? Products { get; set; }
    }
}
