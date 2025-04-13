using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace DemoShop.Models
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required, DisplayName("Product Image")]
        public string Image { get; set; } = string.Empty;
        public bool Featured { get; set; } = false;
        public DateTime DateUploaded { get; set; } = DateTime.Now;
        public int CategoryId { get; set; } 
        public Category Category { get; set; } = null!;
        public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
        public List<CartDetail> CartDetail { get; set; } = new List<CartDetail>();
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
        public List<Favourite> Favorites { get; set; } = new List<Favourite>();
        public Stock Stock { get; set; } = null!;

        [NotMapped]
        public string? CategoryName { get; set; } = string.Empty;
        [NotMapped]
        public int Quantity { get; set; }
    }
}
