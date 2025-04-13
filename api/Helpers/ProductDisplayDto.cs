using DemoShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DemoShop.Helpers
{
    public class ProductDisplayDto
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
        public string STrem { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
