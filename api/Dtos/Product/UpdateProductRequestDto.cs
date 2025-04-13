using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoShop.Dtos.Product
{
    public class UpdateProductRequestDto
    {
        [Required]
        [MaxLength(44, ErrorMessage = "Title cannot be over 44 over characters")]
        public string? Title { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; } = string.Empty;
        [Required, Range(0.1, 99999.99)]
        public decimal Price { get; set; }
        [Required, DisplayName("Product Image")]
        public IFormFile Image { get; set; } = null!;
        public bool Featured { get; set; } = false;

    }
}
