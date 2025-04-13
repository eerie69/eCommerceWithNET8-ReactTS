using api.Dtos.Reviews;
using DemoShop.Dtos.Category;
using DemoShop.Models;

namespace DemoShop.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public bool Featured { get; set; } = false;
        public string CategoryName { get; set; } = string.Empty;
        public DateTime DateUploaded { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public int CartQty { get; set; }
        public List<ReviewsDto> Reviews { get; set; } = new List<ReviewsDto>();
    }
}
