using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api.Models;

namespace DemoShop.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
