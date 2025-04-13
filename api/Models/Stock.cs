using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoShop.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; } = null!;
    }
}
